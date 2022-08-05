using System.Globalization;

namespace CalculatorShell.Expressions.Internals.Conversions
{
    internal class TimeConversions : ConstantConversionBase
    {
        public TimeConversions(CultureInfo culture) : base(culture)
        {
        }

        public override IEnumerable<ConstantBasedConversion> UnitConstants
        {
            get
            {
                yield return new ConstantBasedConversion
                {
                    UnitName = "Second",
                    Value = 1M
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Minute",
                    Value = 60,
                    BaseRelationShip = DivideBase,
                    TargetRelationShip = MultipleOfBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Hour",
                    Value = 3600,
                    BaseRelationShip = DivideBase,
                    TargetRelationShip = MultipleOfBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Day",
                    Value = 86400,
                    BaseRelationShip = DivideBase,
                    TargetRelationShip = MultipleOfBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Week",
                    Value = 604800M,
                    BaseRelationShip = DivideBase,
                    TargetRelationShip = MultipleOfBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Year",
                    Value = 31556925.9936M,
                    BaseRelationShip = DivideBase,
                    TargetRelationShip = MultipleOfBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Beats",
                    Value = 6.4M,
                    BaseRelationShip = DivideBase,
                    TargetRelationShip = MultipleOfBase
                };
            }
        }

        public override string CategoryName => "time";
    }
}
