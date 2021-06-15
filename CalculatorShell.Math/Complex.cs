using System;
using System.Collections.Generic;

namespace CalculatorShell.Maths
{
    public sealed class Complex : IEquatable<Complex>
    {
        private readonly System.Numerics.Complex _complex;

        public double Real => _complex.Real;

        public double Imaginary => _complex.Imaginary;

        public Complex(double real, double imaginary)
        {
            _complex = new System.Numerics.Complex(real, imaginary);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Complex);
        }

        public bool Equals(Complex other)
        {
            return other != null &&
                   _complex.Equals(other._complex);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_complex);
        }

        public static bool operator ==(Complex left, Complex right)
        {
            return EqualityComparer<Complex>.Default.Equals(left, right);
        }

        public static bool operator !=(Complex left, Complex right)
        {
            return !(left == right);
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
    }
}
