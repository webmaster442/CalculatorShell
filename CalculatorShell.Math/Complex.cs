using System;
using System.Collections.Generic;

namespace CalculatorShell.Maths
{
    public sealed class Complex : IEquatable<Complex?>
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

        public static Complex operator + (Complex a, Complex b)
        {
            var calculated = a._complex + b._complex;
            return new Complex(calculated.Real, calculated.Imaginary);
        }

        public static Complex operator +(Complex a, double b)
        {
            var calculated = a._complex + b;
            return new Complex(calculated.Real, calculated.Imaginary);
        }

        public static Complex operator - (Complex a, Complex b)
        {
            var calculated = a._complex - b._complex;
            return new Complex(calculated.Real, calculated.Imaginary);
        }

        public static Complex operator -(Complex a, double b)
        {
            var calculated = a._complex - b;
            return new Complex(calculated.Real, calculated.Imaginary);
        }

        public static Complex operator * (Complex a, Complex b)
        {
            var calculated = a._complex * b._complex;
            return new Complex(calculated.Real, calculated.Imaginary);
        }

        public static Complex operator *(Complex a, double b)
        {
            var calculated = a._complex * b;
            return new Complex(calculated.Real, calculated.Imaginary);
        }

        public static Complex operator / (Complex a, Complex b)
        {
            var calculated = a._complex / b._complex;
            return new Complex(calculated.Real, calculated.Imaginary);
        }

        public static Complex operator / (Complex a, double b)
        {
            var calculated = a._complex / b;
            return new Complex(calculated.Real, calculated.Imaginary);
        }

        public static bool operator ==(Complex? left, Complex? right)
        {
            return EqualityComparer<Complex>.Default.Equals(left, right);
        }

        public static bool operator !=(Complex? left, Complex? right)
        {
            return !(left == right);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Complex);
        }

        public bool Equals(Complex? other)
        {
            return other != null &&
                   _complex.Equals(other._complex);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_complex);
        }
    }
}
