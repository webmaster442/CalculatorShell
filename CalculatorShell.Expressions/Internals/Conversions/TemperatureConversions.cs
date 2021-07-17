using CalculatorShell.Expressions.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CalculatorShell.Expressions.Internals.Conversions
{
    internal class TemperatureConversions : IUnitConverter
    {
        private const string celsius = "celsius";
        private const string farenheit = "farenheit";
        private const string kelvin = "kelvin";

        public TemperatureConversions(CultureInfo culture)
        {
            Culture = culture;
        }

        public IEnumerable<string> KnownUnits
        {
            get
            {
                yield return celsius;
                yield return farenheit;
                yield return kelvin;
            }
        }

        public string CategoryName => "temperature";

        public CultureInfo Culture { get; }

        public string Convert(string inputValue, string inputUnit, string targetUnit)
        {
            if (!KnownUnits.Contains(inputUnit)||
                !KnownUnits.Contains(targetUnit))
            {
                throw new ExpressionEngineException(Resources.CantConvert, inputUnit, targetUnit);
            }

            decimal parsed = ConstantConversionBase.Parse(inputValue, Culture);

            decimal baseValue = 0;

            switch (inputUnit)
            {
                case celsius:
                    baseValue = parsed;
                    break;
                case kelvin:
                    baseValue = parsed - 273.15M;
                    break;
                case farenheit:
                    baseValue = (parsed - 32) / 1.8M;
                    break;
            }

            switch (targetUnit)
            {
                case celsius:
                    break;
                case kelvin:
                    baseValue += 273.15M;
                    break;
                case farenheit:
                    baseValue *= 1.8M;
                    baseValue += 32M;
                    break;
            }

            return baseValue.ToString(Culture);
        }
    }
}
