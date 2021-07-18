﻿using CalculatorShell.Expressions.Properties;
using CalculatorShell.Maths;
using System;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal class Cplx : BinaryExpression
    {
        public Cplx(IExpression? left, IExpression? right, bool polar) : base(left, right)
        {
            Polar = polar;
        }

        public bool Polar { get; }

        public override IExpression Differentiate(string byVariable)
        {
            throw new ExpressionEngineException(Resources.CanotDifferentiate);
        }

        public override IExpression Simplify()
        {
            var newLeft = Left?.Simplify();
            var newRight = Right?.Simplify();

            if (newLeft is Constant leftConst
                && newRight is Constant rightConst)
            {
                // two constants
                return new Constant(Evaluate(leftConst.Value.Value, rightConst.Value.Value));
            }
            else
            {
                return new Cplx(newLeft, newRight, Polar);
            }
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"cplx({Left?.ToString(formatProvider)}; {Right?.ToString(formatProvider)})";
        }

        protected override NumberImplementation Evaluate(NumberImplementation number1, NumberImplementation number2)
        {
            if (Polar)
                return new NumberImplementation(Complex.FromPolar(number1.Value, number2.Value));
            else
                return new NumberImplementation(new Complex(number1.Value, number2.Value));
        }
    }
}
