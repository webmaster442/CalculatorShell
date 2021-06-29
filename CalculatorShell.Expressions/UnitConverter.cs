using CalculatorShell.Expressions.Internals.Conversions;
using CalculatorShell.Expressions.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CalculatorShell.Expressions
{
    /// <summary>
    /// Unit converter class
    /// </summary>
    public sealed class UnitConverter : IUnitConverter
    {
        private IUnitConverter[] _subconverters;

        /// <summary>
        /// Creates a new instance of UnitConverter
        /// </summary>
        /// <param name="cultureInfo">culture to use</param>
        public UnitConverter(CultureInfo cultureInfo)
        {
            Culture = cultureInfo;
            _subconverters = new IUnitConverter[]
            {
                new FileSizeConversion(cultureInfo),
            };
        }

        /// <inheritdoc/>
        public IEnumerable<string> KnownUnits => _subconverters.SelectMany(x => x.KnownUnits);

        /// <inheritdoc/>
        public CultureInfo Culture { get; }

        /// <inheritdoc/>
        public string Convert(string inputValue, string inputUnit, string targetUnit)
        {
            var converter = _subconverters.FirstOrDefault(x => x.KnownUnits.Contains(inputUnit)
                                                            && x.KnownUnits.Contains(targetUnit));

            if (converter == null)
                throw new ExpressionEngineException(Resources.CantConvert, inputUnit, targetUnit);

            return converter.Convert(inputValue, inputUnit, targetUnit);
        }
    }
}
