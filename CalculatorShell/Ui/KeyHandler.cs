namespace CalculatorShell.Ui
{
    internal class KeyHandler
    {
        private readonly StringBuilder _text;
        private readonly List<string> _history;
        private readonly IConsole _console;
        private readonly Dictionary<string, Action> _keyActions;

        private int _cursorPos;
        private int _cursorLimit;
        private int _historyIndex;
        private ConsoleKeyInfo _keyInfo;
        private string[]? _completions;
        private int _completionStart;
        private int _completionsIndex;


        private bool IsStartOfLine() => _cursorPos == 0;

        private bool IsEndOfLine() => _cursorPos == _cursorLimit;

        private bool IsStartOfBuffer() => _console.CursorLeft == 0;

        private bool IsEndOfBuffer() => _console.CursorLeft == _console.BufferWidth - 1;
        private bool IsInAutoCompleteMode() => _completions != null;

        private void MoveCursorLeft()
        {
            if (IsStartOfLine())
                return;

            if (IsStartOfBuffer())
                _console.SetCursorPosition(_console.BufferWidth - 1, _console.CursorTop - 1);
            else
                _console.SetCursorPosition(_console.CursorLeft - 1, _console.CursorTop);

            _cursorPos--;
        }

        private void MoveCursorHome()
        {
            while (!IsStartOfLine())
                MoveCursorLeft();
        }

        private string BuildKeyInput()
        {
            return (_keyInfo.Modifiers is not ConsoleModifiers.Control and not ConsoleModifiers.Shift) ?
                _keyInfo.Key.ToString() : _keyInfo.Modifiers.ToString() + _keyInfo.Key.ToString();
        }

        private void MoveCursorRight()
        {
            if (IsEndOfLine())
                return;

            if (IsEndOfBuffer())
                _console.SetCursorPosition(0, _console.CursorTop + 1);
            else
                _console.SetCursorPosition(_console.CursorLeft + 1, _console.CursorTop);

            _cursorPos++;
        }

        private void MoveCursorEnd()
        {
            while (!IsEndOfLine())
                MoveCursorRight();
        }

        private void ClearLine()
        {
            MoveCursorEnd();
            while (!IsStartOfLine())
                Backspace();
        }

        private void WriteNewString(string str)
        {
            ClearLine();
            foreach (char character in str)
                WriteChar(character);
        }

        private void WriteString(string str)
        {
            foreach (char character in str)
                WriteChar(character);
        }

        private void WriteChar() => WriteChar(_keyInfo.KeyChar);

        private void WriteChar(char c)
        {
            if (IsEndOfLine())
            {
                _text.Append(c);
                _console.Write(c.ToString());
                _cursorPos++;
            }
            else
            {
                int left = _console.CursorLeft;
                int top = _console.CursorTop;
                string str = _text.ToString()[_cursorPos..];
                _text.Insert(_cursorPos, c);
                _console.Write(c.ToString() + str);
                _console.SetCursorPosition(left, top);
                MoveCursorRight();
            }

            _cursorLimit++;
        }

        private void Backspace()
        {
            if (IsStartOfLine())
                return;

            MoveCursorLeft();
            int index = _cursorPos;
            _text.Remove(index, 1);
            string replacement = _text.ToString()[index..];
            int left = _console.CursorLeft;
            int top = _console.CursorTop;
            _console.Write(string.Format("{0} ", replacement));
            _console.SetCursorPosition(left, top);
            _cursorLimit--;
        }

        private void Delete()
        {
            if (IsEndOfLine())
                return;

            int index = _cursorPos;
            _text.Remove(index, 1);
            string replacement = _text.ToString()[index..];
            int left = _console.CursorLeft;
            int top = _console.CursorTop;
            _console.Write(string.Format("{0} ", replacement));
            _console.SetCursorPosition(left, top);
            _cursorLimit--;
        }

        private void TransposeChars()
        {
            // local helper functions
            bool almostEndOfLine() => (_cursorLimit - _cursorPos) == 1;
            int incrementIf(Func<bool> expression, int index) => expression() ? index + 1 : index;
            int decrementIf(Func<bool> expression, int index) => expression() ? index - 1 : index;

            if (IsStartOfLine()) { return; }

            int firstIdx = decrementIf(IsEndOfLine, _cursorPos - 1);
            int secondIdx = decrementIf(IsEndOfLine, _cursorPos);

            (_text[firstIdx], _text[secondIdx]) = (_text[secondIdx], _text[firstIdx]);
            int left = incrementIf(almostEndOfLine, _console.CursorLeft);
            int cursorPosition = incrementIf(almostEndOfLine, _cursorPos);

            WriteNewString(_text.ToString());

            _console.SetCursorPosition(left, _console.CursorTop);
            _cursorPos = cursorPosition;

            MoveCursorRight();
        }

        private void StartAutoComplete()
        {
            while (_cursorPos > _completionStart)
                Backspace();

            _completionsIndex = 0;

            if (_completions != null)
                WriteString(_completions[_completionsIndex]);
        }

        private void NextAutoComplete()
        {
            while (_cursorPos > _completionStart)
                Backspace();

            _completionsIndex++;

            if (_completionsIndex == _completions?.Length)
                _completionsIndex = 0;

            if (_completions != null)
                WriteString(_completions[_completionsIndex]);
        }

        private void PreviousAutoComplete()
        {
            while (_cursorPos > _completionStart)
                Backspace();

            _completionsIndex--;

            if (_completionsIndex == -1)
                _completionsIndex = _completions?.Length - 1 ?? 0;

            if (_completions != null)
                WriteString(_completions[_completionsIndex]);
        }

        private void PrevHistory()
        {
            if (_historyIndex -1 < 0)
            {
                _historyIndex = _history.Count - 1;
            }
            else
            {
                _historyIndex--;
            }
            WriteNewString(_history[_historyIndex]);
        }

        private void NextHistory()
        {
            if (_historyIndex + 1 > _history.Count - 1)
            {
                _historyIndex = 0;
            }
            else
            {
                _historyIndex++;
            }
            WriteNewString(_history[_historyIndex]);
        }

        private void ResetAutoComplete()
        {
            _completions = null;
            _completionsIndex = 0;
        }

        public string Text => _text.ToString();

        public string GetAndResetText()
        {
            string? ret = Text;
            _cursorPos = 0;
            _cursorLimit = 0;
            _text.Clear();
            return ret;
        }

        public KeyHandler(IConsole console, List<string> history, IAutoCompleteHandler autoCompleteHandler)
        {
            _console = console;

            _history = history ?? new List<string>();
            _historyIndex = _history.Count;
            _text = new StringBuilder();
            _keyActions = new Dictionary<string, Action>
            {
                ["LeftArrow"] = MoveCursorLeft,
                ["Home"] = MoveCursorHome,
                ["End"] = MoveCursorEnd,
                ["ControlA"] = MoveCursorHome,
                ["ControlB"] = MoveCursorLeft,
                ["RightArrow"] = MoveCursorRight,
                ["ControlF"] = MoveCursorRight,
                ["ControlE"] = MoveCursorEnd,
                ["Backspace"] = Backspace,
                ["Delete"] = Delete,
                ["ControlD"] = Delete,
                ["ControlH"] = Backspace,
                ["ControlL"] = ClearLine,
                ["Escape"] = ClearLine,
                ["UpArrow"] = PrevHistory,
                ["ControlP"] = PrevHistory,
                ["DownArrow"] = NextHistory,
                ["ControlN"] = NextHistory,
                ["ControlU"] = () =>
                {
                    while (!IsStartOfLine())
                        Backspace();
                },
                ["ControlK"] = () =>
                {
                    int pos = _cursorPos;
                    MoveCursorEnd();
                    while (_cursorPos > pos)
                        Backspace();
                },
                ["ControlW"] = () =>
                {
                    while (!IsStartOfLine() && _text[_cursorPos - 1] != ' ')
                        Backspace();
                },
                ["ControlT"] = TransposeChars,

                ["Tab"] = () =>
                {
                    if (IsInAutoCompleteMode())
                    {
                        NextAutoComplete();
                    }
                    else
                    {
                        if (autoCompleteHandler == null || !IsEndOfLine())
                            return;

                        string text = _text.ToString();

                        _completionStart = text.LastIndexOfAny(autoCompleteHandler.Separators);
                        _completionStart = _completionStart == -1 ? 0 : _completionStart + 1;

                        _completions = autoCompleteHandler.GetSuggestions(text, _completionStart);
                        _completions = _completions?.Length == 0 ? null : _completions;

                        if (_completions == null)
                            return;

                        StartAutoComplete();
                    }
                },

                ["ShiftTab"] = () =>
                {
                    if (IsInAutoCompleteMode())
                    {
                        PreviousAutoComplete();
                    }
                }
            };
        }

        public void Handle(ConsoleKeyInfo keyInfo)
        {
            _keyInfo = keyInfo;

            // If in auto complete mode and Tab wasn't pressed
            if (IsInAutoCompleteMode() && _keyInfo.Key != ConsoleKey.Tab)
                ResetAutoComplete();

            Action? action;
            _keyActions.TryGetValue(BuildKeyInput(), out action);
            action = action ?? WriteChar;
            action.Invoke();
        }
    }
}
