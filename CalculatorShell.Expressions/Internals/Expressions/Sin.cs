using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal sealed class Sin : UnaryExpression
    {
        public Sin(IExpression? child) : base(child)
        {
        }

        public override IExpression? Differentiate(string byVariable)
        {
            return new Multiply(new Cos(Child), Child?.Differentiate(byVariable));
        }

        public override IExpression? Simplify()
        {
            var newChild = Child?.Simplify();
            if (newChild is Constant childConst)
            {
                // child is constant
                return new Constant(Evaluate(childConst.Value));
            }
            return new Sin(newChild);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"sin({Child?.ToString(formatProvider)})";
        }

        protected override NumberImplementation Evaluate(NumberImplementation number)
        {
            //TODO: Proper system switcher
            return Math.Sin(number.Value);
        }
    }
}
