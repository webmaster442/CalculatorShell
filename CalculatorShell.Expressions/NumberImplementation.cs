using CalculatorShell.Maths;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Linq;

namespace CalculatorShell.Expressions
{
    internal class NumberImplementation : INumber
    {
        public dynamic Value { get; }
        
        internal bool IsInteger
        {
            get
            {
                return NumberType == NumberType.Double
                    && Maths.Extensions.IsInteger(Value);
            }
        }

        public NumberImplementation(double n, double d)
        {
            NumberType = NumberType.Fraction;
            Value = new Fraction(n.ToInteger(), d.ToInteger());
        }

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
                default:
                    NumberType = NumberType.Object;
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

        internal string ToString(IFormatProvider formatProvider)
        {
            return Value.ToString(formatProvider);
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

        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }

        public IDictionary<string, string> GetObjectData()
        {
            object o = Value;
            Dictionary<string, string> result = new();
            var properties = o.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                result.Add(property.Name, property.GetValue(o)?.ToString() ?? string.Empty);
            }
            return result;
        }

        public INumber GetPropertyValue(string property)
        {
            object o = Value;
            var propertyInfo = o.GetType()?
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(x => x.Name.Equals(property, StringComparison.InvariantCultureIgnoreCase));
            if (propertyInfo != null)
            {
                var result = propertyInfo?.GetValue(o);
                if (result != null)
                {
                    return new NumberImplementation(result);
                }

            }
            return new NumberImplementation(double.NaN);
        }
    }
}
