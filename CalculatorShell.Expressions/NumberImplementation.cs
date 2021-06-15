using CalculatorShell.Maths;
using System.Globalization;

namespace CalculatorShell.Expressions
{
    internal class NumberImplementation : INumber
    {
        public dynamic Value { get; }

        public NumberImplementation(dynamic value)
        {
            Value = value;
            switch (value)
            {
                case double:
                    NumberType = NumberType.Double;
                    break;
                case System.Numerics.Complex:
                    NumberType = NumberType.Complex;
                    break;
                case Fraction:
                    NumberType = NumberType.Fraction;
                    break;
                case bool:
                    NumberType = NumberType.Boolean;
                    break;
            }
        }

        public NumberType NumberType { get; }

        public bool GetBooean()
        {
            if (NumberType != NumberType.Boolean)
                throw new TypeException("Value is not a boolean");

            return (bool)Value;
        }

        public System.Numerics.Complex GetComplex()
        {
            if (NumberType != NumberType.Complex)
                throw new TypeException("Value is not a Complex");

            var v = (Maths.Complex)Value;
            return new System.Numerics.Complex(v.Real, v.Imaginary);
        }

        public double GetDouble()
        {
            if (NumberType != NumberType.Double)
                throw new TypeException("Value is not a double");

            return (double)Value;
        }

        public (long numerator, long denominator) GetFraction()
        {
            if (NumberType != NumberType.Fraction)
                throw new TypeException("Value is not a Fraction");

            var v = (Fraction)Value;
            return (v.Numerator, v.Denominator);

        }

        public string ToString(CultureInfo cultureInfo)
        {
            return Value.ToString(cultureInfo);
        }
    }
}
