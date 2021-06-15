using CalculatorShell.Expressions.Properties;
using System;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal sealed class Not : UnaryExpression
    {
        public Not(IExpression? child) : base(child)
        {
        }

        public override IExpression? Differentiate(string byVariable)
        {
            throw new ExpressionEngineException(Resources.CanotDifferentiate);
        }

        public override IExpression? Simplify()
        {
            var newChild = Child?.Simplify();
            if (newChild is Constant childConst)
            {
                // child is constant
                return new Constant(new NumberImplementation(!childConst.Value.Value));
            }
            return new Not(newChild);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"(!{Child?.ToString(formatProvider)})";
        }

        protected override NumberImplementation Evaluate(NumberImplementation number)
        {
            return new NumberImplementation(!number.Value.Value);
        }
    }
}
