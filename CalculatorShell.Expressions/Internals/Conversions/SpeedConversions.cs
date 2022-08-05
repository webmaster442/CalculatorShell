using System.Globalization;

namespace CalculatorShell.Expressions.Internals.Conversions
{
    internal class SpeedConversions : ConstantConversionBase
    {
        public SpeedConversions(CultureInfo culture) : base(culture)
        {
        }

        public override IEnumerable<ConstantBasedConversion> UnitConstants
        {
            get
            {
                yield return new ConstantBasedConversion
                {
                    UnitName = "Meters/second",
                    Value = 1M
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Meters/minute",
                    Value = 60,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Meters/hour",
                    Value = 3600,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Feet/second",
                    Value = 3.280839895M,
                    BaseRelationShip =
                    MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Feet/minute",
                    Value = 196.8503937M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Feet/hour",
                    Value = 11811.02362M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Knots",
                    Value = 1.943844492M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Mach",
                    Value = 0.003016955M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Miles/second",
                    Value = 0.000621371M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Miles/minute",
                    Value = 0.037282272M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Miles/hour",
                    Value = 2.236936292M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
            }
        }

        public override string CategoryName => "speed";
    }
}
