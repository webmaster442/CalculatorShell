namespace CalculatorShell.Ui
{
    //TODO: Investigate why up key doesn't work sometimes
    internal sealed class LineReader
    {
        private readonly List<string> _history;
        private readonly KeyHandler _keyHandler;
        private readonly IConsole _console;

        public bool HistoryEnabled { get; }

        public LineReader(bool enableHistory, IConsole console, IAutoCompleteHandler autoCompleteHandler)
        {
            _console = console;
            _history = new List<string>(30);
            HistoryEnabled = enableHistory;
            _keyHandler = new KeyHandler(_console, _history, autoCompleteHandler);
        }

        public void AddHistory(params string[] text) => _history.AddRange(text);
        public IList<string> GetHistory() => _history;
        public void ClearHistory() => _history.Clear();

        public string Read(string prompt = "", string @default = "")
        {
            _console.Write(prompt);
            string text = GetText(_keyHandler);

            if (string.IsNullOrWhiteSpace(text)
                && !string.IsNullOrWhiteSpace(@default))
            {
                text = @default;
            }
            else
            {
                if (HistoryEnabled)
                    _history.Add(text);
            }

            return text;
        }

        private string GetText(KeyHandler keyHandler)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                keyHandler.Handle(keyInfo);
                keyInfo = Console.ReadKey(true);
            }
            _console.WriteLine(string.Empty);
            return keyHandler.GetAndResetText();
        }
    }
}
