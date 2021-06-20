using System;

namespace CalculatorShell.Infrastructure
{
    internal interface IConsole : IProgress<int>
    {
        void Error(Exception ex);
        void Write(string format, params object[] args);
        void WriteLine(string format, params object[] args);
        void WriteObjectJson(object obj);
        void Clear();
    }
}
