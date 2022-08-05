using System.Globalization;

namespace CalculatorShell.Expressions
{
    /// <summary>
    /// An interface to acces expresion result
    /// </summary>
    public interface INumber
    {
        /// <summary>
        /// Number type information
        /// </summary>
        NumberType NumberType { get; }
        /// <summary>
        /// Converts the number to a string with a given culture
        /// </summary>
        /// <param name="cultureInfo">Culture to use</param>
        /// <returns>String representation of the number</returns>
        string ToString(CultureInfo cultureInfo);
        /// <summary>
        /// Converts the number to a string
        /// </summary>
        /// <returns>String representation of the number</returns>
        string ToString();
        /// <summary>
        /// Convert the number to a System.Double
        /// </summary>
        /// <see cref="System.Double"/>
        /// <returns>A System.Double</returns>
        double GetDouble();
        /// <summary>
        /// Convert the number to a Value tuple describing a fraction
        /// </summary>
        /// <returns>Value tuple describing a fraction</returns>
        (long numerator, long denominator) GetFraction();
        /// <summary>
        /// Convert the number to a System.Numerics.Complex
        /// </summary>
        /// <see cref="System.Numerics.Complex"/>
        /// <returns>System.Numerics.Complex instance</returns>
        System.Numerics.Complex GetComplex();
        /// <summary>
        /// Convert the number to a System.Boolean
        /// </summary>
        /// <see cref="System.Boolean"/>
        /// <returns>System.Boolean instance</returns>
        bool GetBooean();
        /// <summary>
        /// Get the numbers properties and values in string representation
        /// </summary>
        /// <returns>Object properties as dictionary</returns>
        IDictionary<string, string> GetObjectData();
        /// <summary>
        /// Get a property of the underlying type as a Number
        /// </summary>
        /// <param name="property">property to get</param>
        /// <returns>Property value to acces</returns>
        INumber GetPropertyValue(string property);
        /// <summary>
        /// Convert the number to a Value tuple describing a 2d Vector
        /// </summary>
        /// <returns>Value tuple describing a fraction</returns>
        (double x, double y) GetVector2();
        /// <summary>
        /// Convert the number to a Value tuple describing a 3d Vector
        /// </summary>
        /// <returns>Value tuple describing a fraction</returns>
        (double x, double y, double z) GetVector3();
    }
}
