using CalculatorShell.Maths;
using System;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal class Cplx : TypeFunction<Complex>
    {
        public bool Polar { get; }

        public Cplx(bool polar, params IExpression[] expressions) : base(expressions)
        {
            Polar = polar;
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"cplx({Parameters[0]?.ToString(formatProvider)}; {Parameters[1]?.ToString(formatProvider)})";
        }

        protected override TypeFunction<Complex> CreateExpression(IExpression[] parameters)
        {
            return new Cplx(Polar, parameters);
        }

        protected override NumberImplementation Evaluate(params NumberImplementation[] numbers)
        {
            if (Polar)
                return new NumberImplementation(Complex.FromPolar(numbers[0].Value, numbers[1].Value));
            else
                return new NumberImplementation(new Complex(numbers[0].Value, numbers[1].Value));
        }
    }
}
