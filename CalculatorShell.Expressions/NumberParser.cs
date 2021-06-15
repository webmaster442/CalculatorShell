using CalculatorShell.Maths;
using System.Collections.Generic;
using System.Globalization;

namespace CalculatorShell.Expressions
{
    internal static class NumberParser
    {
        private static Dictionary<string, double> _multipliers = new()
        {
            { "p", 1E-12 },
            { "n", 1E-9 },
            { "micro", 1E-6 },
            { "m", 1E-3 },
            { "d", 1E1 },
            { "h", 1E2 },
            { "k", 1E3 },
            { "M", 1E6 },
            { "G", 1E9 },
            { "T", 1E12 },
        };

        private static string FindPrefix(string input)
        {
            foreach (var prefix in _multipliers.Keys)
            {
                if (input.Contains(prefix))
                    return prefix;
            }
            return string.Empty;
        }

        public static NumberImplementation Parse(string input, CultureInfo culture)
        {
            string prefix = FindPrefix(input);

            if (string.Compare(input, "true", true, culture) == 0)
            {
                return new NumberImplementation(true);
            }
            else if (string.Compare(input, "false", true, culture) == 0)
            {
                return new NumberImplementation(false);
            }
            else if (!string.IsNullOrEmpty(prefix))
            {
                double number = double.Parse(input.Replace(prefix, ""), culture);
                return new NumberImplementation(number * _multipliers[prefix]);
            }
            else
            {
                double number = double.Parse(input, culture);
                return new NumberImplementation(number);
            }
            throw new TypeException("Can't parse number");
        }
    }
}
