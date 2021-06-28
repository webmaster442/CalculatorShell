using CalculatorShell.Expressions;

namespace CalculatorShell.Base
{
    public interface IMemory : IVariables
    {
        void Delete(string name);
    }
}
