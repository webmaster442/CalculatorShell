using CalculatorShell.Base;
using CalculatorShell.Expressions;
using CalculatorShell.Properties;
using CalculatorShell.Ui;
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
        private readonly IHostEx _host;
        private readonly IFsHost _fsHost;

        public CommandRunner(IEnumerable<ICommand> commands,
                             IHostEx host,
                             IFsHost fsHost,
                             CultureInfo culture)
        {
            _culture = culture;
            _console = new ProgramConsole();
            _console.InterruptRequested += OnInterruptRequested;
            _lineReader = new LineReader(true, _console, this);
            _commandTable = CreateCommandsTable(commands);
            _cancellationTokenSource = new CancellationTokenSource();
            _host = host;
            _fsHost = fsHost;
            SetupHosting();
        }

        private Dictionary<string, ICommand> CreateCommandsTable(IEnumerable<ICommand> commands)
        {
            var ret = new Dictionary<string, ICommand>();
            foreach (var cmd in commands)
            {
                ret.Add(cmd.GetType().Name.ToLower(), cmd);
                foreach (var alias in cmd.Aliases)
                {
                    ret.Add(alias.ToLower(), cmd);
                }
            }
            return ret;
        }

        private void SetupHosting()
        {
            _host.SetCommands(_commandTable.Keys);
            _host.SetFunctions(ExpressionFactory.KnownFunctions);
        }

        private void OnInterruptRequested(object? sender, EventArgs e)
        {
            _cancellationTokenSource?.Cancel();
        }

        public async Task RunShell()
        {
            while (_host.CanRun)
            {
                string prompt = CreatePrompt(_fsHost.CurrentDirectory);
                string rawLine = _lineReader.Read(prompt);

                await RunSingleCommand(rawLine, _console).ConfigureAwait(false);
            }
        }

        public async Task RunSingleCommand(string rawLine, ICommandConsole console)
        {
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

                            simpleCommand.Execute(new Arguments(args, _culture), console);
                        }
                        else if (_commandTable[cmd] is ITaskCommand taskCommand
                            && _cancellationTokenSource != null)
                        {
                            _console.WriteLine(Resources.BackgroundJobStart);
                            await taskCommand.Execute(new Arguments(args, _culture), console, _cancellationTokenSource.Token).ConfigureAwait(false);
                            ResetToken();
                        }
                        else if (_commandTable[cmd] is IFsTaskCommand fsTaskCommand
                            && _cancellationTokenSource != null)
                        {
                            _console.WriteLine(Resources.BackgroundJobStart);
                            await fsTaskCommand.Execute(new Arguments(args, _culture), console, _fsHost, _cancellationTokenSource.Token).ConfigureAwait(false);
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

        private static string CreatePrompt(string currentDirectory)
        {
            var angleMode = EscapeCodeFactory.CreateFormatSting(new ConsoleFormat
            {
                TextFormat = TextFormat.Italic,
                Foreground = new Base.ConsoleColor(0xff, 0x00, 0xff),
            });

            var dir = EscapeCodeFactory.CreateFormatSting(new ConsoleFormat
            {
                Foreground = new Base.ConsoleColor(0xF2, 0xC5, 0x1F),
                TextFormat = TextFormat.None,
            });

            return $"\n{dir}{currentDirectory}{EscapeCodeFactory.Reset}\n"
                   + $"→ {angleMode}{ExpressionFactory.CurrentAngleMode}{EscapeCodeFactory.Reset} > ";
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
                .Where(c => c.StartsWith(word, StringComparison.OrdinalIgnoreCase))
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
