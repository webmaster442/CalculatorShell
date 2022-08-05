namespace CalculatorShell.Expressions
{
    /// <summary>
    /// A parsed expression
    /// </summary>
    public interface IExpression
    {
        /// <summary>
        /// Expression variables.
        /// </summary>
        IVariables? Variables { get; }
        /// <summary>
        /// Differentiate the Expression
        /// </summary>
        /// <param name="byVariable">variable name to use</param>
        /// <returns>Differentiated expression</returns>
        IExpression Differentiate(string byVariable);
        /// <summary>
        /// Simplify the expression
        /// </summary>
        /// <returns>Simplified expression</returns>
        IExpression Simplify();
        /// <summary>
        /// Evaluate the expression
        /// </summary>
        /// <returns></returns>
        INumber Evaluate();
        /// <summary>
        /// Convert the expression to string with given format
        /// </summary>
        /// <param name="formatProvider">format provider to use</param>
        /// <returns>string representation of the expression</returns>
        /// <see cref="System.IFormatProvider"/>
        /// <see cref="System.Globalization.CultureInfo"/>
        string ToString(IFormatProvider formatProvider);
    }
}
