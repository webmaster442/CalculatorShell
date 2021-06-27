using CalculatorShell.Expressions;

namespace CalculatorShell.Base
{
    public interface IMemory : IVariables
    {
        void Delete(string name);
        void WriteToFile(string fileName);
        void ReadFromFile(string fileName);
    }
}
