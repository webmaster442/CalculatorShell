using CalculatorShell.Expressions.Properties;
using CalculatorShell.Maths;
using System;
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
            var builder = new StringBuilder(1024);
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
                    var fraction = number.GetFraction();
                    builder.AppendFormat("{0};{1}", fraction.numerator.ToString(CultureInfo.InvariantCulture), fraction.denominator.ToString(CultureInfo.InvariantCulture));
                    break;
                case NumberType.Vector2:
                    var vect2 = number.GetVector2();
                    builder.AppendFormat("{0};{1}", GetString(vect2.x), GetString(vect2.y));
                    break;
                case NumberType.Vector3:
                    var vect3 = number.GetVector3();
                    builder.AppendFormat("{0};{1};{2}", GetString(vect3.x), GetString(vect3.y), GetString(vect3.z));
                    break;
                case NumberType.Complex:
                    var cplx = number.GetComplex();
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
            var tokens = value.Split(";");
            if (tokens.Length < 1)
                throw new ExpressionEngineException(Resources.SerializationError);

            var type = Enum.Parse<NumberType>(tokens[0], true);

            try
            {
                switch (type)
                {
                    case NumberType.Boolean:
                        return new NumberImplementation(bool.Parse(tokens[1]));
                    case NumberType.Complex:
                        return new NumberImplementation(new Complex(double.Parse(tokens[1], CultureInfo.InvariantCulture), double.Parse(tokens[2], CultureInfo.InvariantCulture)));
                    case NumberType.Double:
                        return new NumberImplementation(double.Parse(tokens[1], CultureInfo.InvariantCulture));
                    case NumberType.Fraction:
                        return new NumberImplementation(new Fraction(long.Parse(tokens[1], CultureInfo.InvariantCulture), long.Parse(tokens[2], CultureInfo.InvariantCulture)));
                    case NumberType.Vector2:
                        return new NumberImplementation(new Vector2(double.Parse(tokens[1], CultureInfo.InvariantCulture), double.Parse(tokens[2], CultureInfo.InvariantCulture)));
                    case NumberType.Vector3:
                        return new NumberImplementation(new Vector3(double.Parse(tokens[1], CultureInfo.InvariantCulture), double.Parse(tokens[2], CultureInfo.InvariantCulture), double.Parse(tokens[3], CultureInfo.InvariantCulture)));
                    case NumberType.Object:
                        return new NumberImplementation(new NotSerializedObject());
                    default:
                        throw new ExpressionEngineException(Resources.SerializationError);
                }
            }
            catch (Exception e)
            {
                throw new ExpressionEngineException(Resources.SerializationError, e);
            }

        }
    }
}
