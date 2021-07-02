using CalculatorShell.Expressions;
using System.Collections.Generic;

namespace CalculatorShell.Base
{
    /// <summary>
    /// Provides API for variable management.
    /// </summary>
    public interface IMemory : IVariables
    {
        /// <summary>
        /// Removes a variable or expression by it's name
        /// </summary>
        /// <param name="name">Variable name to remove</param>
        void Delete(string name);

        /// <summary>
        /// Store an expression for later use
        /// </summary>
        /// <param name="name">Name of expression</param>
        /// <param name="parsed">parsed expression</param>
        void SetExpression(string name, IExpression parsed);
        /// <summary>
        /// Get a stored expression
        /// </summary>
        /// <param name="name">expression name</param>
        /// <returns>Parsed expression</returns>
        IExpression GetExpression(string name);
        /// <summary>
        /// Get a collection of currently known expression names
        /// </summary>
        IEnumerable<string> ExpressionNames { get; }
    }
}
