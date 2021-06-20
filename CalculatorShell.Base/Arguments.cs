using System;
using System.Globalization;

namespace CalculatorShell.Base
{
    public sealed class Arguments
    {
        private readonly string[] _arguments;
        private readonly CultureInfo _culture;

        public Arguments(string[] args, CultureInfo culture)
        {
            _arguments = args;
            _culture = culture;
        }

        public T Get<T>(int index, string failMsg = null) where T : IConvertible
        {
            try
            {
                return (T)Convert.ChangeType(_arguments[index], typeof(T), _culture);
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(failMsg))
                    throw new InvalidOperationException(ex.Message, ex);
                else
                    throw new InvalidOperationException(failMsg, ex);
            }
        }

        public int Count => _arguments.Length;
    }
}
