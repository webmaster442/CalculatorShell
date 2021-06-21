using System.Collections.Generic;

namespace CalculatorShell.Expressions
{
    public interface IVariables
    {
        bool IsConstant(string variableName);
        bool IsVariable(string variableName);
        bool IsVariableOrConstant(string variableName) => IsConstant(variableName) || IsVariable(variableName);
        dynamic this[string variable] { get; set; }
        void Clear();
        int Count { get; }
        IEnumerable<string> VariableAndConstantNames { get; }
        IEnumerable<string> VariableNames { get; }
    }
}
