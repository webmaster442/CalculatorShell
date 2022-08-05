using System.Text;

namespace CalculatorShell.Expressions.Internals.Logic
{
    internal static class ImplicantStringFactory
    {
        public static string Create(Implicant implicant, int length, bool lsba, bool negate)
        {
            string? mask = implicant.Mask.PadLeft(length, '0');
            string result = string.Empty;
            if (lsba)
                result = CreateLsb(negate, mask, length);
            else
                result = CreateMsb(negate, mask);

            if (!string.IsNullOrWhiteSpace(result))
                return $"({result[0..^1]})";

            return result;
        }

        private static string CreateMsb(bool negate, string mask)
        {
            StringBuilder builder = new();
            for (int i = 0; i < mask.Length; i++)
            {
                if (negate)
                {
                    if (mask[i] == '0') builder.AppendFormat("{0} |", GetChar(i));
                    else if (mask[i] == '1') builder.AppendFormat("!{0} |", GetChar(i));
                }
                else
                {
                    if (mask[i] == '0') builder.AppendFormat("!{0} &", GetChar(i));
                    else if (mask[i] == '1') builder.AppendFormat("{0} &", GetChar(i));
                }
            }
            return builder.ToString();
        }

        private static string CreateLsb(bool negate, string mask, int length)
        {
            StringBuilder builder = new();
            for (int i = 0; i < mask.Length; i++)
            {
                if (negate)
                {
                    if (mask[i] == '0') builder.AppendFormat("{0} |", GetChar(length, i));
                    else if (mask[i] == '1') builder.AppendFormat("!{0} |", GetChar(length, i));
                }
                else
                {
                    if (mask[i] == '0') builder.AppendFormat("!{0} &", GetChar(length, i));
                    else if (mask[i] == '1') builder.AppendFormat("{0} &", GetChar(length, i));
                }
            }

            return builder.ToString();
        }

        private static char GetChar(int length, int i)
        {
            return Convert.ToChar('A' + (length - 1) - i);
        }

        private static char GetChar(int i)
        {
            return Convert.ToChar('A' + i);
        }
    }
}
