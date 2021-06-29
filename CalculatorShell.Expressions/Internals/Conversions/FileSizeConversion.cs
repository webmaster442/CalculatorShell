using System.Collections.Generic;
using System.Globalization;

namespace CalculatorShell.Expressions.Internals.Conversions
{
    internal class FileSizeConversion : ConstantConversionBase
    {
        public FileSizeConversion(CultureInfo culture) : base(culture)
        {
        }

        public override IEnumerable<ConstantBasedConversion> UnitConstants
        {
            get
            {
                yield return new ConstantBasedConversion
                {
                    Value = 1M,
                    UnitName = "byte",
                };
                yield return new ConstantBasedConversion
                {
                    Value = 1024M,
                    UnitName = "kiB",
                    ToBaseUnit = MultiplyValue,
                    ToTargetUnit = DivideValue
                };
                yield return new ConstantBasedConversion
                {
                    Value = 1048576M,
                    UnitName = "MiB",
                    ToBaseUnit = MultiplyValue,
                    ToTargetUnit = DivideValue
                };
                yield return new ConstantBasedConversion
                {
                    Value = 1073741824M,
                    UnitName = "GiB",
                    ToBaseUnit = MultiplyValue,
                    ToTargetUnit = DivideValue
                };
                yield return new ConstantBasedConversion
                {
                    Value = 1099511627776M,
                    UnitName = "TiB",
                    ToBaseUnit = MultiplyValue,
                    ToTargetUnit = DivideValue
                };
                yield return new ConstantBasedConversion
                {
                    Value = 1125899906842624M,
                    UnitName = "PiB",
                    ToBaseUnit = MultiplyValue,
                    ToTargetUnit = DivideValue
                };
            }
        }
    }
}
