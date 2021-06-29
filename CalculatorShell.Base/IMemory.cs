using CalculatorShell.Expressions;

namespace CalculatorShell.Base
{
    /// <summary>
    /// Provides API for variable management.
    /// </summary>
    public interface IMemory : IVariables
    {
        /// <summary>
        /// Removes a variable by it's name
        /// </summary>
        /// <param name="name">Variable name to remove</param>
        void Delete(string name);
    }
}
