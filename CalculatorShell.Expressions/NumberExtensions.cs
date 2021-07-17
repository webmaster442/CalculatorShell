namespace CalculatorShell.Expressions
{
    /// <summary>
    /// Number related extensions
    /// </summary>
    public static class NumberExtensions
    {
        /// <summary>
        /// Create an INumber from a double
        /// </summary>
        /// <param name="input">double value</param>
        /// <returns>INumber created from the input value</returns>
        public static INumber CreateNumber(this double input)
        {
            return new NumberImplementation(input);
        }

        /// <summary>
        /// Create an INumber from a boolean
        /// </summary>
        /// <param name="input">boolean value</param>
        /// <returns>INumber created from the input value</returns>
        public static INumber CreateNumber(this bool input)
        {
            return new NumberImplementation(input);
        }

        /// <summary>
        /// Create an INumber from an object
        /// </summary>
        /// <param name="input">object value</param>
        /// <returns>INumber created from the input value</returns>
        public static INumber CreateNumber(this object input)
        {
            return new NumberImplementation(input);
        }
    }
}
