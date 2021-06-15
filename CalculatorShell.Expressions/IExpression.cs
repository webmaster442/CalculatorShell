using System;

namespace CalculatorShell.Expressions
{
    public interface IExpression
    {
        IVariables? Variables { get; }
        IExpression? Differentiate(string byVariable);
        IExpression? Simplify();
        INumber Evaluate();
        string ToString(IFormatProvider formatProvider);
    }
}
