using System;

namespace CalculatorShell.Base
{
    public interface ICommandConsole : IProgress<int>
    {
        void Error(Exception ex);
        void Error(string format, params object[] args);
        void Write(string format, params object[] args);
        void WriteLine(string format, params object[] args);
        void WriteObjectJson(object obj);
        void Clear();
    }
}
