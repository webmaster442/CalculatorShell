using CalculatorShell.Expressions.Properties;
using CalculatorShell.Maths;
using System.Globalization;
using System.Text;

namespace CalculatorShell.Expressions
{
    /// <summary>
    /// Number serialization helpers
    /// </summary>
    public static class NumberSerializerFactory
    {
        private static string GetString(bool input)
        {
            return input.ToString(CultureInfo.InvariantCulture);
        }

        private static string GetString(double input)
        {
            return input.ToString("G17", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Serializes a number to string
        /// </summary>
        /// <param name="number">number to serialize</param>
        /// <returns>serialized value</returns>
        public static string Serialize(INumber number)
        {
            StringBuilder? builder = new StringBuilder(1024);
            builder.AppendFormat("{0};", number.NumberType.ToString());
            switch (number.NumberType)
            {
                case NumberType.Boolean:
                    builder.AppendFormat("{0}", GetString(number.GetBooean()));
                    break;
                case NumberType.Double:
                    builder.AppendFormat("{0}", GetString(number.GetDouble()));
                    break;
                case NumberType.Fraction:
                    (long numerator, long denominator) = number.GetFraction();
                    builder.AppendFormat("{0};{1}", numerator.ToString(CultureInfo.InvariantCulture), denominator.ToString(CultureInfo.InvariantCulture));
                    break;
                case NumberType.Vector2:
                    (double x, double y) = number.GetVector2();
                    builder.AppendFormat("{0};{1}", GetString(x), GetString(y));
                    break;
                case NumberType.Vector3:
                    (double x, double y, double z) vect3 = number.GetVector3();
                    builder.AppendFormat("{0};{1};{2}", GetString(vect3.x), GetString(vect3.y), GetString(vect3.z));
                    break;
                case NumberType.Complex:
                    System.Numerics.Complex cplx = number.GetComplex();
                    builder.AppendFormat("{0};{1}", GetString(cplx.Real), GetString(cplx.Imaginary));
                    break;
                case NumberType.Object:
                    builder.Append("");
                    break;
            }
            return builder.ToString();
        }

        /// <summary>
        /// Deserializes a number from string
        /// </summary>
        /// <param name="value">string value to deserialize</param>
        /// <returns>Deserialized value</returns>
        public static INumber Deserialize(string value)
        {
            string[]? tokens = value.Split(";");
            if (tokens.Length < 1)
                throw new ExpressionEngineException(Resources.SerializationError);

            NumberType type = Enum.Parse<NumberType>(tokens[0], true);

            try
            {
                return type switch
                {
                    NumberType.Boolean => new NumberImplementation(bool.Parse(tokens[1])),
                    NumberType.Complex => new NumberImplementation(new Complex(double.Parse(tokens[1], CultureInfo.InvariantCulture), double.Parse(tokens[2], CultureInfo.InvariantCulture))),
                    NumberType.Double => new NumberImplementation(double.Parse(tokens[1], CultureInfo.InvariantCulture)),
                    NumberType.Fraction => new NumberImplementation(new Fraction(long.Parse(tokens[1], CultureInfo.InvariantCulture), long.Parse(tokens[2], CultureInfo.InvariantCulture))),
                    NumberType.Vector2 => new NumberImplementation(new Vector2(double.Parse(tokens[1], CultureInfo.InvariantCulture), double.Parse(tokens[2], CultureInfo.InvariantCulture))),
                    NumberType.Vector3 => new NumberImplementation(new Vector3(double.Parse(tokens[1], CultureInfo.InvariantCulture), double.Parse(tokens[2], CultureInfo.InvariantCulture), double.Parse(tokens[3], CultureInfo.InvariantCulture))),
                    NumberType.Object => new NumberImplementation(new NotSerializedObject()),
                    _ => throw new ExpressionEngineException(Resources.SerializationError),
                };
            }
            catch (Exception e)
            {
                throw new ExpressionEngineException(Resources.SerializationError, e);
            }

        }
    }
}
