using System;

namespace CalculatorShell.Base
{
    public sealed class Arguments
    {
        private readonly string[] _arguments;

        public Arguments(string[] args)
        {
            _arguments = args;
        }

        public T Get<T>(int index) where T : IConvertible
        {
            return (T)Convert.ChangeType(_arguments[index], typeof(T));
        }

        public int Count => _arguments.Length;
    }
}
