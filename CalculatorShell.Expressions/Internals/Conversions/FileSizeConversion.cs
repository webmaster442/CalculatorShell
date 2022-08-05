using System.Globalization;

namespace CalculatorShell.Expressions.Internals.Conversions
{
    internal sealed class FileSizeConversion : ConstantConversionBase
    {
        public FileSizeConversion(CultureInfo culture) : base(culture)
        {
        }

        public override string CategoryName => "File size";

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
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    Value = 1048576M,
                    UnitName = "MiB",
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    Value = 1073741824M,
                    UnitName = "GiB",
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    Value = 1099511627776M,
                    UnitName = "TiB",
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    Value = 1125899906842624M,
                    UnitName = "PiB",
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
            }
        }
    }
}
