﻿using System.Globalization;

namespace CalculatorShell.Expressions
{
    public interface INumber
    {
        NumberType NumberType { get; }
        string ToString(CultureInfo cultureInfo);
        double GetDouble();
        (long numerator, long denominator) GetFraction();
        System.Numerics.Complex GetComplex();
        bool GetBooean();
    }
}
