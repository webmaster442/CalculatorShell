namespace CalculatorShell.Base
{
    /// <summary>
    /// Console color
    /// </summary>
    public sealed record ConsoleColor
    {
        /// <summary>
        /// Creates a new instance of color
        /// </summary>
        public ConsoleColor() 
        {
            R = 0xff;
            G = 0xff;
            B = 0xff;
        }

        /// <summary>
        /// Creates a new instance of color
        /// </summary>
        /// <param name="r">red value</param>
        /// <param name="g">green value</param>
        /// <param name="b">blue value</param>
        public ConsoleColor(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

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
