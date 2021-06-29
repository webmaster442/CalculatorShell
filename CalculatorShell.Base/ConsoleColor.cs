namespace CalculatorShell.Base
{
    /// <summary>
    /// Console color
    /// </summary>
    public sealed record ConsoleColor
    {
        /// <summary>
        /// Red
        /// </summary>
        public byte R { get; init; }
        /// <summary>
        /// Green
        /// </summary>
        public byte G { get; init; }
        /// <summary>
        /// Blue
        /// </summary>
        public byte B { get; init; }
    }
}
