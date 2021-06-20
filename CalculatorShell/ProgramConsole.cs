using System;
using System.Text.Json;

namespace CalculatorShell
{
    internal class ProgramConsole : CalculatorShell.Infrastructure.IConsole, CalculatorShell.ReadLine.IConsole
    {
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
            WriteLine("Error: {0}", ex.Message);
        }

        public void Report(int value)
        {
            if (value < 0)
                value = 0;
            if (value > 100)
                value = 100;
            WriteLine("Progress: {0}%", value);
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

        public void Write(string value) => Write(value, null);

        public void Write(string format, params object[] args)
        {
            Console.Write(format, args);
        }

        public void WriteLine(string value) => WriteLine(value, null);

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
