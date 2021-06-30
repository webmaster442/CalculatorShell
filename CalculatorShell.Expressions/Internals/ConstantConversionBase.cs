using CalculatorShell.Expressions.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CalculatorShell.Expressions.Internals
{
    internal abstract class ConstantConversionBase : IUnitConverter
    {
        private readonly Dictionary<string, decimal> _prefixTable;

        public ConstantConversionBase(CultureInfo culture)
        {
            Culture = culture;
            _prefixTable = new Dictionary<string, decimal>
            {
                { "zetta",    1E21M },
                { "Z",        1E21M },
                { "exa",      1E18M },
                { "E",        1E18M },
                { "peta",     1E15M },
                { "P",        1E15M },
                { "tera",     1E12M },
                { "T",        1E12M },
                { "giga",     1E9M },
                { "G",        1E9M },
                { "mega",     1E6M },
                { "M",        1E6M },
                { "kilo",     1E3M },
                { "k",        1E3M },
                { "hecto",    1E2M },
                { "h",        1E2M },
                { "deca",     1E1M },
                { "da",       1E1M },
                { "deci",     1E-1M },
                { "d",        1E-1M },
                { "centi",    1E-2M },
                { "c",        1E-2M },
                { "mili",     1E-3M },
                { "m",        1E-3M },
                { "micro",    1E-6M },
                { "u",        1E-6M },
                { "nano",     1E-9M },
                { "n",        1E-9M },
                { "pico",     1E-12M },
                { "p",        1E-12M },
                { "femto",    1E-15M },
                { "f",        1E-15M },
                { "atto",     1E-18M },
                { "a",        1E-18M },
                { "zepto",    1E-21M },
                { "z",        1E-21M },
            };
        }

        public IEnumerable<string> KnownUnits => UnitConstants.Select(x => x.UnitName);

        public CultureInfo Culture { get; }

        public abstract IEnumerable<ConstantBasedConversion> UnitConstants { get; }
        
        public abstract string CategoryName { get; }

        public string Convert(string inputValue, string inputUnit, string targetUnit)
        {
            decimal sourceValue = Parse(inputValue);
            ConstantBasedConversion? unitSource = GetUnit(inputUnit, out decimal sourcePrefix);
            ConstantBasedConversion? unitTarget = GetUnit(targetUnit, out decimal targetPrefix);

            if (unitSource == null
                || unitTarget == null)
            {
                throw new ExpressionEngineException(Resources.CantConvert, inputUnit, targetUnit);
            }

            decimal inBase = ConvertToBaseUnit(sourceValue, unitSource, sourcePrefix);
            decimal inTarget = ConvertToTargetUnit(inBase, unitTarget, targetPrefix);

            return inTarget.ToString(Culture);
        }

        private decimal ConvertToTargetUnit(decimal inBase, ConstantBasedConversion unitTarget, decimal prefix)
        {
            if (unitTarget.IsBaseUnit && prefix == 1M)
                return inBase;

            return unitTarget.TargetRelationShip(inBase, unitTarget.Value, prefix);
        }

        private decimal ConvertToBaseUnit(decimal sourceValue, ConstantBasedConversion unitSource, decimal pefix)
        {
            if (unitSource.IsBaseUnit && pefix == 1M)
                return sourceValue;

            return unitSource.BaseRelationShip(sourceValue, unitSource.Value, pefix);
        }

        private ConstantBasedConversion? GetUnit(string inputUnit, out decimal sourceMultiplier)
        {
            if (inputUnit.Contains("_"))
            {
                var parts = inputUnit.Split('_');
                sourceMultiplier = _prefixTable[parts[0]];
                return UnitConstants.FirstOrDefault(x => x.UnitName == parts[1]);
            }
            sourceMultiplier = 1M;
            return UnitConstants.FirstOrDefault(x => x.UnitName == inputUnit);
        }

        private decimal Parse(string inputValue)
        {
            try
            {
                return decimal.Parse(inputValue, Culture);
            }
            catch (Exception ex)
            {
                throw new ExpressionEngineException(Resources.NumberParseFailed, ex);
            }
        }

        protected decimal DivideBase(decimal rawValue, decimal constantMultiplier, decimal prefix)
        {
            if (prefix == 1)
                return rawValue / constantMultiplier;
            else
                return rawValue / constantMultiplier / prefix;
        }

        protected decimal MultipleOfBase(decimal rawValue, decimal constantMultiplier, decimal prefix)
        {
            return rawValue * constantMultiplier * prefix;
        }
    }
}
