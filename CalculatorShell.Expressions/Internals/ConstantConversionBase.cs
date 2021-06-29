using CalculatorShell.Expressions.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

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

        public string Convert(string inputValue, string inputUnit, string targetUnit)
        {
            decimal sourceValue = Parse(inputValue);
            ConstantBasedConversion? unitSource = GetUnit(inputUnit);
            ConstantBasedConversion? unitTarget = GetUnit(targetUnit);

            if (unitSource == null
                || unitTarget == null)
            {
                throw new ExpressionEngineException(Resources.CantConvert, inputUnit, targetUnit);
            }

            decimal inBase = ConvertToBaseUnit(sourceValue, unitSource);
            decimal inTarget = ConvertToTargetUnit(inBase, unitTarget);

            return inTarget.ToString(Culture);
        }

        private decimal ConvertToTargetUnit(decimal inBase, ConstantBasedConversion unitTarget)
        {
            if (unitTarget.IsBaseUnit)
                return inBase;

            return unitTarget.TargetRelationShip(inBase, unitTarget.Value);
        }

        private decimal ConvertToBaseUnit(decimal sourceValue, ConstantBasedConversion unitSource)
        {
            if (unitSource.IsBaseUnit)
                return sourceValue;

            return unitSource.BaseRelationShip(sourceValue, unitSource.Value);
        }

        private ConstantBasedConversion? GetUnit(string inputUnit)
        {
            return UnitConstants.FirstOrDefault(x => x.UnitName == inputUnit);
        }

        private decimal Parse(string inputValue)
        {
            try
            {
                decimal multiplier = 1M;
                if (inputValue.Contains('_'))
                {
                    string[] parts = inputValue.Split('_');
                    if (_prefixTable.ContainsKey(parts[0]))
                    {
                        multiplier = _prefixTable[parts[0]];
                    }
                    StringBuilder numbers = new();
                    for (int i = 1; i < parts.Length; i++)
                    {
                        numbers.Append(parts[i]);
                    }
                    decimal v = decimal.Parse(numbers.ToString(), Culture);
                    return v * multiplier;
                }
                return decimal.Parse(inputValue, Culture);
            }
            catch (Exception ex)
            {
                throw new ExpressionEngineException(Resources.NumberParseFailed, ex);
            }
        }

        protected decimal DivideBase(decimal arg1, decimal arg2)
        {
            return arg1 / arg2;
        }

        protected decimal MultipleOfBase(decimal arg1, decimal arg2)
        {
            return arg1 * arg2;
        }
    }
}
