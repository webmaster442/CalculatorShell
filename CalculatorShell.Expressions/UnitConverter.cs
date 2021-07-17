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
    public sealed class UnitConverter
    {
        private readonly IUnitConverter[] _subconverters;

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
                new LengthConversions(cultureInfo),
                new PowerConversions(cultureInfo),
                new FlowConversions(cultureInfo),
                new AreaConversions(cultureInfo),
                new AccelerationConversions(cultureInfo),
                new SpeedConversions(cultureInfo),
                new PressureConversions(cultureInfo),
                new MassConversions(cultureInfo),
                new VolumeConversions(cultureInfo),
                new TimeConversions(cultureInfo),
            };
        }

        /// <inheritdoc/>
        public IEnumerable<string> KnownUnits => _subconverters.SelectMany(x => x.KnownUnits);

        /// <inheritdoc/>
        public CultureInfo Culture { get; }

        /// <inheritdoc/>
        public string Convert(string inputValue, string inputUnit, string targetUnit)
        {
            string searchInputUnit = inputUnit;
            string searchOutputUnit = targetUnit;

            if (inputUnit.Contains('_'))
                searchInputUnit = inputUnit.Split('_')[1];

            if (targetUnit.Contains('_'))
                searchOutputUnit = targetUnit.Split('_')[1];

            var converter = _subconverters.FirstOrDefault(x => x.KnownUnits.Contains(searchInputUnit)
                                                            && x.KnownUnits.Contains(searchOutputUnit));

            if (converter == null)
                throw new ExpressionEngineException(Resources.CantConvert, inputUnit, targetUnit);

            return converter.Convert(inputValue, inputUnit, targetUnit);
        }
    }
}
