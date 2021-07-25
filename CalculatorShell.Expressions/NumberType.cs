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
        /// the number is a 2d vector
        /// </summary>
        Vector2,
        /// <summary>
        /// the number is a 3d vector
        /// </summary>
        Vector3,
        /// <summary>
        /// The number is an object
        /// </summary>
        Object,
    }
}
