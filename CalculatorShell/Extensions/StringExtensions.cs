using CalculatorShell.Ui;

namespace CalculatorShell.Extensions
{
    internal static class StringExtensions
    {
        public static IEnumerable<string> ParseArguments(this string commandLine)
        {
            if (string.IsNullOrWhiteSpace(commandLine))
                yield break;

            StringBuilder? buffer = new StringBuilder(1024);
            bool inQuote = false;
            foreach (char c in commandLine)
            {
                if (c == '"' && !inQuote)
                {
                    inQuote = true;
                    continue;
                }

                if (c != '"' && !(char.IsWhiteSpace(c) && !inQuote))
                {
                    buffer.Append(c);
                    continue;
                }

                if (buffer.Length > 0)
                {
                    string? result = buffer.ToString();
                    buffer.Clear();
                    inQuote = false;
                    yield return result;
                }
            }

            if (buffer.Length > 0)
                yield return buffer.ToString();
        }

        public static string CreatePrompt(this string currentDirectory)
        {
            string? angleMode = EscapeCodeFactory.CreateFormatSting(new ConsoleFormat
            {
                TextFormat = TextFormat.Italic,
                Foreground = new Base.ConsoleColor(0xff, 0x00, 0xff),
            });

            string? dir = EscapeCodeFactory.CreateFormatSting(new ConsoleFormat
            {
                Foreground = new Base.ConsoleColor(0xF2, 0xC5, 0x1F),
                TextFormat = TextFormat.None,
            });

            return $"\n{dir}{currentDirectory}{EscapeCodeFactory.Reset}\n"
                   + $"→ {angleMode}{ExpressionFactory.CurrentAngleMode}{EscapeCodeFactory.Reset} > ";
        }
    }
}
