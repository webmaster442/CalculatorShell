using System;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal sealed class Ln : UnaryExpression
    {
        public Ln(IExpression? child) : base(child)
        {
        }

        public override IExpression? Differentiate(string byVariable)
        {
            return new Divide(new Constant(new NumberImplementation(1)), Child);
        }

        public override IExpression? Simplify()
        {
            var newChild = Child?.Simplify();
            if (newChild is Constant childConst)
            {
                // child is constant
                return new Constant(Evaluate(childConst.Value));
            }
            return new Ln(newChild);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"ln({Child?.ToString(formatProvider)})";
        }

        protected override NumberImplementation Evaluate(NumberImplementation number)
        {
            return Math.Log(number.Value);
        }
    }
}
