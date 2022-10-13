namespace CalculatorShell.Ui
{
    internal static class EscapeCodeFactory
    {
        public const string ClearScreen = "\x1b[2J\x1b[0;0H";
        public const string Reset = "\x1B[0m";

        public static string CreateFormatSting(ConsoleFormat format)
        {
            StringBuilder buffer = new StringBuilder(80);

            if (format.Background != null)
            {
                buffer.AppendFormat("\x1b[48;2;{0};{1};{2}m",
                                    format.Background.R,
                                    format.Background.G,
                                    format.Background.B);
            }

            if (format.Foreground != null)
            {
                buffer.AppendFormat("\x1b[38;2;{0};{1};{2}m",
                                    format.Foreground.R,
                                    format.Foreground.G,
                                    format.Foreground.B);
            }

            switch (format.TextFormat)
            {
                case TextFormat.None:
                    break;
                case TextFormat.Bold:
                    buffer.Append("\x1b[1m");
                    break;
                case TextFormat.Italic:
                    buffer.Append("\x1b[3m");
                    break;
                case TextFormat.Underline:
                    buffer.Append("\x1b[4m");
                    break;
                case TextFormat.Strikethrough:
                    buffer.Append("\x1b[9m");
                    break;
            }

            return buffer.ToString();
        }

    }
}
