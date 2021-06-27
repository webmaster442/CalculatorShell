using CalculatorShell.Base;
using CalculatorShell.Properties;
using CalculatorShell.ReadLine;
using System;
using System.Text.Json;

namespace CalculatorShell
{
    internal class ProgramConsole : ICommandConsole, IConsole
    {
        public ProgramConsole()
        {
            Console.CancelKeyPress += Console_CancelKeyPress;
        }

        private void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            InterruptRequested?.Invoke(this, EventArgs.Empty);
        }

        private static void WriteWithColors(ConsoleColor color, Action action)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            action.Invoke();
            Console.ForegroundColor = currentColor;
        }

        public event EventHandler? InterruptRequested;

        public int CursorLeft => Console.CursorLeft;

        public int CursorTop => Console.CursorTop;

        public int BufferWidth => Console.BufferWidth;

        public int BufferHeight => Console.BufferHeight;

        public void Clear()
        {
            Console.Clear();
        }

        public void Error(Exception ex)
        {
            WriteWithColors(ConsoleColor.Red, () => WriteLine(Resources.GeneralError, ex.Message));
        }

        public void Error(string format, params object[] args)
        {
            WriteWithColors(ConsoleColor.Red, () => WriteLine(format, args));
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
            Console.Write(format, args);
        }

        public void WriteLine(string value) => WriteLine(value, Array.Empty<object>());

        public void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void WriteObjectJson(object obj)
        {
            string result = JsonSerializer.Serialize(obj, obj.GetType(), new JsonSerializerOptions
            {
                WriteIndented = true
            });
            WriteLine(result);
        }
    }
}
