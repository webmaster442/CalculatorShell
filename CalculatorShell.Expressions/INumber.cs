using System.Collections.Generic;
using System.Globalization;

namespace CalculatorShell.Expressions
{
    public interface INumber
    {
        NumberType NumberType { get; }
        string ToString(CultureInfo cultureInfo);
        string ToString();
        double GetDouble();
        (long numerator, long denominator) GetFraction();
        System.Numerics.Complex GetComplex();
        bool GetBooean();
        IDictionary<string, object?> GetObjectData();
        string GetPropertyValue(string property);
    }
}
