using System;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal class Abs : UnaryExpression
    {
        public Abs(IExpression? child) : base(child)
        {
        }

        public override IExpression Differentiate(string byVariable)
        {
            return new Divide(Child?.Differentiate(byVariable), new Abs(Child));
        }

        public override IExpression Simplify()
        {
            var newChild = Child?.Simplify();
            if (newChild is Constant childConst)
            {
                // child is constant
                return new Constant(Evaluate(childConst.Value));
            }
            return new Abs(newChild);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"abs({Child?.ToString(formatProvider)})";
        }

        protected override NumberImplementation Evaluate(NumberImplementation number)
        {
            return new NumberImplementation(Math.Abs(number.Value));
        }
    }
}
