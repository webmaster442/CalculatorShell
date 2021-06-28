using System;
using System.Collections.Generic;

namespace CalculatorShell.Base
{
    public interface ICommandConsole : IProgress<float>, IProgress<TimeSpan>
    {
        void Error(Exception ex);
        void Error(string format, params object[] args);
        void Write(string format, params object[] args);
        void WriteLine(string format, params object[] args);
        void WriteTable<T>(T item);
        void WriteTable<Tkey, TValue>(IDictionary<Tkey, TValue> dictionary);
        void WriteTable<T>(IEnumerable<T> items, int columns = 4);
        void Clear();
        ConsoleFormat? CurrentFormat { get; set; }
    }
}