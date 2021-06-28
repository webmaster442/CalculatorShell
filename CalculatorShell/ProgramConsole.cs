using CalculatorShell.Base;
using CalculatorShell.Properties;
using CalculatorShell.Ui;
using System;
using System.Collections.Generic;

namespace CalculatorShell
{
    internal class ProgramConsole : ICommandConsole, IConsole
    {
        private ConsoleFormat? _currentFormat;

        private static readonly ConsoleFormat ErrorFormat = new()
        {
            TextFormat = TextFormat.Bold,
            Foreground = new Base.ConsoleColor
            {
                R = 0xFF,
                G = 0x00,
                B = 0x00,
            }
        };

        public ProgramConsole()
        {
            Console.CancelKeyPress += Console_CancelKeyPress;
        }

        private void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            InterruptRequested?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler? InterruptRequested;

        public int CursorLeft => Console.CursorLeft;

        public int CursorTop => Console.CursorTop;

        public int BufferWidth => Console.BufferWidth;

        public int BufferHeight => Console.BufferHeight;

        public void Clear()
        {
            Console.Write(EscapeCodeFactory.ClearScreen);
            Console.Write(EscapeCodeFactory.Reset);
        }

        public void Error(Exception ex)
        {
            SetFormat(ErrorFormat);
            WriteLine(Resources.GeneralError, ex.Message);
            ClearFormat();
        }

        public void Error(string format, params object[] args)
        {
            SetFormat(ErrorFormat);
            WriteLine(format, args);
            ClearFormat();
        }

        public void Report(float value)
        {
            if (value < 0)
                value = 0;
            if (value > 1)
                value = 1;
            WriteLine("Progress: {0:P}%", value);
        }

        public void Report(TimeSpan value)
        {
            WriteLine("Time passed: {0}", value);
        }

        public void SetBufferSize(int width, int height)
        {
            if (OperatingSystem.IsWindows())
            {
                Console.SetBufferSize(width, height);
            }
        }

        public void SetCursorPosition(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }

        public void Write(string value) => Write(value, Array.Empty<object>());

        public void Write(string format, params object[] args)
        {
            if (_currentFormat != null)
                Console.Write(EscapeCodeFactory.CreateFormatSting(_currentFormat));

            Console.Write(format, args);
        }

        public void WriteLine(string value) => WriteLine(value, Array.Empty<object>());

        public void WriteLine(string format, params object[] args)
        {
            if (_currentFormat != null)
                Console.Write(EscapeCodeFactory.CreateFormatSting(_currentFormat));

            Console.WriteLine(format, args);
        }

        public void WriteTable<T>(T item)
        {
            Console.WriteLine(TableHelper.WriteTable(item));
        }

        public void WriteTable<Tkey, TValue>(IDictionary<Tkey, TValue> dictionary)
        {
            Console.WriteLine(TableHelper.WriteTable(dictionary));
        }

        public void WriteTable<T>(IEnumerable<T> items, int columns = 4)
        {
            Console.WriteLine(TableHelper.WriteTable(items, columns));
        }

        public void SetFormat(ConsoleFormat format)
        {
            _currentFormat = format;
        }

        public void ClearFormat()
        {
            Write(EscapeCodeFactory.Reset);
            _currentFormat = null;
        }
    }
}
