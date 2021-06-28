namespace CalculatorShell.Base
{
    public sealed record ConsoleFormat
    {
        public TextFormat TextFormat { get; init; }
        public ConsoleColor? Foreground { get; init; }
        public ConsoleColor? Background { get; init; }
    }
}
