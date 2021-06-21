using CalculatorShell.Maths;
using System;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal sealed class Tan : UnaryExpression
    {
        public Tan(IExpression? child) : base(child)
        {
        }

        public override IExpression Differentiate(string byVariable)
        {
            return new Multiply(new Exponent(new Cos(Child), new Constant(new NumberImplementation(-2))), Child?.Differentiate(byVariable));
        }

        public override IExpression Simplify()
        {
            var newChild = Child?.Simplify();
            if (newChild is Constant childConst)
            {
                // child is constant
                return new Constant(Evaluate(childConst.Value));
            }
            return new Tan(newChild);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"tan({Child?.ToString(formatProvider)})";
        }

        protected override NumberImplementation Evaluate(NumberImplementation number)
        {
            return new NumberImplementation(Trigonometry.Tan(number.Value));
        }
    }
}
