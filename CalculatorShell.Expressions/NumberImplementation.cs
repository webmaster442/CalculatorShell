using CalculatorShell.Maths;
using System.Globalization;
using System.Reflection;

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
            NumberType = value switch
            {
                double => NumberType.Double,
                System.Numerics.Complex => NumberType.Complex,
                Fraction => NumberType.Fraction,
                bool => NumberType.Boolean,
                Vector2 => NumberType.Vector2,
                Vector3 => NumberType.Vector3,
                _ => NumberType.Object,
            };
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

            Complex v = (Maths.Complex)Value;
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

            Fraction v = (Fraction)Value;
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
            PropertyInfo[]? properties = o.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo? property in properties)
            {
                result.Add(property.Name, property.GetValue(o)?.ToString() ?? string.Empty);
            }
            return result;
        }

        public INumber GetPropertyValue(string property)
        {
            object o = Value;
            PropertyInfo? propertyInfo = o.GetType()?
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(x => x.Name.Equals(property, StringComparison.InvariantCultureIgnoreCase));
            if (propertyInfo != null)
            {
                object? result = propertyInfo.GetValue(o);
                if (result != null)
                {
                    return new NumberImplementation(result);
                }

            }
            return new NumberImplementation(double.NaN);
        }

        public (double x, double y) GetVector2()
        {
            Vector2 v = (Vector2)Value;
            return (v.X, v.Y);
        }

        public (double x, double y, double z) GetVector3()
        {
            Vector3 v = (Vector3)Value;
            return (v.X, v.Y, v.Z);
        }
    }
}
