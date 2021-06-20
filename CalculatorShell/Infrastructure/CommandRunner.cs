using CalculatorShell.Base;
using CalculatorShell.Properties;
using CalculatorShell.ReadLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalculatorShell.Infrastructure
{
    internal sealed class CommandRunner : IAutoCompleteHandler, IDisposable
    {
        private readonly LineReader _lineReader;
        private readonly ProgramConsole _console;
        private readonly Dictionary<string, ICommand> _commandTable;
        private string _currentPrompt;
        private CancellationTokenSource _cancellationTokenSource;

        public CommandRunner(IEnumerable<ICommand> commands)
        {
            _currentPrompt = "Calculator >";
            _console = new ProgramConsole();
            _console.InterruptRequested += _console_InterruptRequested;
            _lineReader = new LineReader(true, _console, this);
            _commandTable = commands.ToDictionary(x => x.GetType().Name, x => x);
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void _console_InterruptRequested(object sender, EventArgs e)
        {
            _cancellationTokenSource.Cancel();
        }

        public async Task Run()
        {
            while (true)
            {
                string rawLine = _lineReader.Read(_currentPrompt);
                IEnumerable<string> arguments = ParseArguments(rawLine);
                string cmd = arguments.FirstOrDefault();
                string[] args = arguments.Skip(1).ToArray();

                if (!string.IsNullOrEmpty(cmd))
                {
                    if (_commandTable.ContainsKey(cmd))
                    {
                        try
                        {
                            if (_commandTable[cmd] is ISimpleCommand simpleCommand)
                            {
                                simpleCommand.Execute(new Arguments(args), _console);
                            }
                            else if (_commandTable[cmd] is ITaskCommand taskCommand)
                            {
                                await taskCommand.Execute(new Arguments(args), _console, _cancellationTokenSource.Token);
                            }
                            else
                            {
                                throw new InvalidOperationException($"Don't know how to run command: {cmd}");
                            }
                        }
                        catch (Exception ex)
                        {
                            _console.Error(ex);
                        }
                    }
                    else
                    {
                        _console.Error(Resources.UnknownCommand, cmd);
                    }
                }

            }
        }

        private static IEnumerable<string> ParseArguments(string commandLine)
        {
            if (string.IsNullOrWhiteSpace(commandLine))
                yield break;

            var buffer = new StringBuilder(1024);
            bool inQuote = false;
            foreach (char c in commandLine)
            {
                if (c == '"' && !inQuote)
                {
                    inQuote = true;
                    continue;
                }

                if (c != '"' && !(char.IsWhiteSpace(c) && !inQuote))
                {
                    buffer.Append(c);
                    continue;
                }

                if (buffer.Length > 0)
                {
                    var result = buffer.ToString();
                    buffer.Clear();
                    inQuote = false;
                    yield return result;
                }
            }

            if (buffer.Length > 0)
                yield return buffer.ToString();
        }

        char[] IAutoCompleteHandler.Separators => new char[] { ' ' };

        string[] IAutoCompleteHandler.GetSuggestions(string text, int index)
        {
            return _commandTable.Keys
                .Where(x => x.IndexOf(text, index, StringComparison.InvariantCultureIgnoreCase) >= 0)
                .OrderBy(x => x.IndexOf(text, index, StringComparison.InvariantCultureIgnoreCase))
                .ToArray();
        }

        public void Dispose()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }
    }
}
