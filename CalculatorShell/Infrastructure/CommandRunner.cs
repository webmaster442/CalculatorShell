using CalculatorShell.Base;
using CalculatorShell.Expressions;
using CalculatorShell.Properties;
using CalculatorShell.ReadLine;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly CultureInfo _culture;
        private CancellationTokenSource? _cancellationTokenSource;
        private readonly HostEnvironment _host;

        public CommandRunner(IEnumerable<ICommand> commands, HostEnvironment host, CultureInfo culture)
        {
            _culture = culture;
            _console = new ProgramConsole();
            _console.InterruptRequested += OnInterruptRequested;
            _lineReader = new LineReader(true, _console, this);
            _commandTable = commands.ToDictionary(x => x.GetType().Name, x => x);
            _cancellationTokenSource = new CancellationTokenSource();
            _host = host;
            SetupHosting();
        }

        private void SetupHosting()
        {
            _host.Commands = _commandTable.Keys;
            _host.Functions = ExpressionFactory.KnownFunctions;
        }

        private void OnInterruptRequested(object? sender, EventArgs e)
        {
            _cancellationTokenSource?.Cancel();
        }

        public async Task Run()
        {
            while (true)
            {
                string prompt = $"\n{ExpressionFactory.CurrentAngleMode} > ";
                string rawLine = _lineReader.Read(prompt);
                IEnumerable<string> arguments = ParseArguments(rawLine);
                string cmd = arguments.FirstOrDefault() ?? string.Empty;
                string[] args = arguments.Skip(1).ToArray();

                if (!string.IsNullOrEmpty(cmd))
                {
                    if (_commandTable.ContainsKey(cmd))
                    {
                        try
                        {
                            if (_commandTable[cmd] is ISimpleCommand simpleCommand)
                            {
                                simpleCommand.Execute(new Arguments(args, _culture), _console);
                            }
                            else if (_commandTable[cmd] is ITaskCommand taskCommand
                                && _cancellationTokenSource != null)
                            {
                                _console.WriteLine(Resources.BackgroundJobStart);
                                await taskCommand.Execute(new Arguments(args, _culture), _console, _cancellationTokenSource.Token).ConfigureAwait(false);
                                ResetToken();
                            }
                            else
                            {
                                // Missing entry proint for command
#if DEBUG
                                System.Diagnostics.Debugger.Break();
#endif
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

        private void ResetToken()
        {
            if (_cancellationTokenSource?.IsCancellationRequested == true)
            {
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
                _cancellationTokenSource = new CancellationTokenSource();
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
            string word = text[index..];

            return _commandTable.Keys
                .Where(c => c.Contains(word, StringComparison.OrdinalIgnoreCase))
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
