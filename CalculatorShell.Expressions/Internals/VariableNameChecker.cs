using System.Text.RegularExpressions;

namespace CalculatorShell.Expressions.Internals
{
    internal static class VariableNameChecker
    {
        private static readonly Regex validator = new(@"^[a-z1-9_]+$");

        public static bool IsValidVariableName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            if (char.IsNumber(name[0]))
                return false;

            if (name.Contains("[") && name.Contains("]"))
                name = name.Replace("[", "]").Replace("]", "");

            return validator.IsMatch(name);
        }
    }
}
