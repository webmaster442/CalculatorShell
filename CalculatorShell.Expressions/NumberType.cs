namespace CalculatorShell.Expressions
{
    /// <summary>
    /// Number type
    /// </summary>
    public enum NumberType
    {
        /// <summary>
        /// The number is an IEEE-754 64 bit floating point number
        /// </summary>
        Double,
        /// <summary>
        /// The number is a complex number
        /// </summary>
        Complex,
        /// <summary>
        /// The number is a fraction
        /// </summary>
        Fraction,
        /// <summary>
        /// The number is a boolean
        /// </summary>
        Boolean,
        /// <summary>
        /// The number is an object
        /// </summary>
        Object,
    }
}
