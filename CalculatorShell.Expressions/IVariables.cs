using System.Collections.Generic;

namespace CalculatorShell.Expressions
{
    public interface IVariables
    {
        bool IsConstant(string variableName);
        bool IsVaribleDefined(string variableName);
        bool IsVariableOrConstant(string variableName) => IsConstant(variableName) || IsVaribleDefined(variableName);
        dynamic this[string variable] { get; set; }
        void Clear();
        int Count { get; }
        IEnumerable<string> VariableNames { get; }
    }
}
