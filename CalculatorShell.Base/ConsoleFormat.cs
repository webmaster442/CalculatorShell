namespace CalculatorShell.Base
{
    /// <summary>
    /// Virtual terminal console formatting
    /// </summary>
    public sealed record ConsoleFormat
    {
        /// <summary>
        /// Text format to use
        /// </summary>
        public TextFormat TextFormat { get; init; }
        /// <summary>
        /// Foreground color. If it's null, then foreground color isn't changed.
        /// </summary>
        public ConsoleColor? Foreground { get; init; }
        /// <summary>
        /// Background color. If it's null, then background color isn't changed.
        /// </summary>
        public ConsoleColor? Background { get; init; }
    }
}
