namespace CalculatorShell.Expressions
{
    /// <summary>
    /// Provides acces to variable and constant management
    /// </summary>
    public interface IVariables
    {
        /// <summary>
        /// Check if the variable name is a constant
        /// </summary>
        /// <param name="variableName">varialbe name to check</param>
        /// <returns>true, if the variable is a constant</returns>
        bool IsConstant(string variableName);
        /// <summary>
        /// Check if the variable name is a variable
        /// </summary>
        /// <param name="variableName">varialbe name to check</param>
        /// <returns>true, if the variable is a variable</returns>
        bool IsVariable(string variableName);
        /// <summary>
        /// Gets a variable value.
        /// </summary>
        /// <param name="variable">Variable to get</param>
        /// <returns>variable value</returns>
        INumber this[string variable] { get; set; }
        /// <summary>
        /// Gets a variables property.
        /// </summary>
        /// <param name="variable">Variable to use</param>
        /// <param name="property">Property to acces</param>
        /// <returns>property value</returns>
        INumber this[string variable, string property] { get; }

        /// <summary>
        /// Clear all variables
        /// </summary>
        void Clear();

        /// <summary>
        /// Returns the collection of currently set variable names. Constants are excluded
        /// </summary>
        IEnumerable<string> VariableNames { get; }
    }
}
