using System.Globalization;

namespace CalculatorShell.Maths
{
    public struct Complex : ICalculatorType
    {
        private readonly System.Numerics.Complex _complex;

        public double Real => _complex.Real;

        public double Imaginary => _complex.Imaginary;

        public double AbsoluteValue => _complex.Magnitude;

        public Complex(double real, double imaginary)
        {
            _complex = new System.Numerics.Complex(real, imaginary);
        }

        public static Complex FromPolar(double absolutevalue, double angle)
        {
            double r = absolutevalue * Trigonometry.Cos(angle);
            double i = absolutevalue * Trigonometry.Sin(angle);
            return new Complex(r, i);
        }

        public static Complex operator +(Complex a, Complex b)
        {
            System.Numerics.Complex calculated = a._complex + b._complex;
            return new Complex(calculated.Real, calculated.Imaginary);
        }

        public static Complex operator +(Complex a, double b)
        {
            System.Numerics.Complex calculated = a._complex + b;
            return new Complex(calculated.Real, calculated.Imaginary);
        }

        public static Complex operator -(Complex a, Complex b)
        {
            System.Numerics.Complex calculated = a._complex - b._complex;
            return new Complex(calculated.Real, calculated.Imaginary);
        }

        public static Complex operator -(Complex a, double b)
        {
            System.Numerics.Complex calculated = a._complex - b;
            return new Complex(calculated.Real, calculated.Imaginary);
        }

        public static Complex operator *(Complex a, Complex b)
        {
            System.Numerics.Complex calculated = a._complex * b._complex;
            return new Complex(calculated.Real, calculated.Imaginary);
        }

        public static Complex operator *(Complex a, double b)
        {
            System.Numerics.Complex calculated = a._complex * b;
            return new Complex(calculated.Real, calculated.Imaginary);
        }

        public static Complex operator /(Complex a, Complex b)
        {
            System.Numerics.Complex calculated = a._complex / b._complex;
            return new Complex(calculated.Real, calculated.Imaginary);
        }

        public static Complex operator /(Complex a, double b)
        {
            System.Numerics.Complex calculated = a._complex / b;
            return new Complex(calculated.Real, calculated.Imaginary);
        }

        public static bool operator ==(Complex left, Complex right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Complex left, Complex right)
        {
            return !(left == right);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Complex complex)
                return Equals(complex);
            return false;
        }

        public bool Equals(Complex other)
        {
            return  _complex.Equals(other._complex);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_complex);
        }

        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }

        public string ToString(IFormatProvider format)
        {
            return $"({_complex.Real.ToString(format)} + {_complex.Imaginary.ToString(format)}i)";
        }
    }
}
