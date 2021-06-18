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
            { "_p", 1E-12 },
            { "_n", 1E-9 },
            { "_micro", 1E-6 },
            { "_m", 1E-3 },
            { "_d", 1E1 },
            { "_h", 1E2 },
            { "_k", 1E3 },
            { "_M", 1E6 },
            { "_G", 1E9 },
            { "_T", 1E12 },
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

        private static string PreprocessInput(string input, bool isDate)
        {
            if (input.Contains('_'))
            {
                if (isDate) return input.Replace('_', ' ');
                else return input.Replace("_", "");
            }
            return input;
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
                double n = double.Parse(PreprocessInput(input.Replace(prefix, ""), false), culture);
                number = new NumberImplementation(n * _multipliers[prefix]);
                return true;
            }
            else if ((input.Contains('_') || input.Contains('/') || input.Contains(':'))
                && DateTime.TryParse(PreprocessInput(input, true), culture, DateTimeStyles.AssumeUniversal, out DateTime parsed))
            {
                number = new NumberImplementation(parsed.ToUnixTime());
                return true;
            }
            else if (double.TryParse(PreprocessInput(input, false), NumberStyles.Any, culture, out double n))
            {
                number = new NumberImplementation(n);
                return true;
            }
            number = new NumberImplementation(double.NaN);
            return false;
        }
    }
}
