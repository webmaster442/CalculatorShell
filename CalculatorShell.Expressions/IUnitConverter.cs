using System.Globalization;

namespace CalculatorShell.Expressions
{
    /// <summary>
    /// Unit converter interface
    /// </summary>
    public interface IUnitConverter
    {
        /// <summary>
        /// Collection of known units that can be used for converting
        /// </summary>
        IEnumerable<string> KnownUnits { get; }

        /// <summary>
        /// Return conversion category name
        /// </summary>
        string CategoryName { get; }

        /// <summary>
        /// Current culture of the converter
        /// </summary>
        CultureInfo Culture { get; }

        /// <summary>
        /// Convert a value to a target value
        /// </summary>
        /// <param name="inputValue">string representation of input value to convert</param>
        /// <param name="inputUnit">string representation of input unit to convert</param>
        /// <param name="targetUnit">string representation of target unit</param>
        /// <returns>input value represented in target unit represented as a string</returns>
        string Convert(string inputValue, string inputUnit, string targetUnit);
    }
}
