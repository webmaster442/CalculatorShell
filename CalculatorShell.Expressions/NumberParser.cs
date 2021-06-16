using CalculatorShell.Maths;
using System;
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

        public static bool TryParse(string input, out NumberImplementation number, CultureInfo culture)
        {
            string prefix = FindPrefix(input);

            if (string.Compare(input, "true", true, culture) == 0)
            {
                number = new NumberImplementation(true);
                return true;
            }
            else if (string.Compare(input, "false", true, culture) == 0)
            {
                number = new NumberImplementation(false);
                return true;
            }
            else if (!string.IsNullOrEmpty(prefix))
            {
                double n = double.Parse(input.Replace(prefix, ""), culture);
                number = new NumberImplementation(n * _multipliers[prefix]);
                return true;
            }
            else if ((input.Contains("-") || input.Contains(":"))
                && DateTime.TryParse(input, culture, DateTimeStyles.AssumeUniversal, out DateTime parsed))
            {
                number = new NumberImplementation(parsed.ToUnixTime());
                return true;
            }
            else if (double.TryParse(input, NumberStyles.Any, culture, out double n))
            {
                number = new NumberImplementation(n);
                return true;
            }
            number = new NumberImplementation(double.NaN);
            return false;
        }
    }
}
