using System.Globalization;

namespace CalculatorShell.Expressions.Internals.Conversions
{
    internal class PressureConversions : ConstantConversionBase
    {
        public PressureConversions(CultureInfo culture) : base(culture)
        {
        }

        public override IEnumerable<ConstantBasedConversion> UnitConstants
        {
            get
            {
                yield return new ConstantBasedConversion
                {
                    UnitName = "Pascals",
                    Value = 1M
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Atmospheres",
                    Value = 9.87E-06M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Bars",
                    Value = 1.00E-05M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Pounds/Foot2",
                    Value = 0.020885434M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Pounds/Inch2",
                    Value = 0.000145038M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Tons/Foot2",
                    Value = 1.04E-05M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Tons/Inch2",
                    Value = 7.25E-08M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Kilograms/Meter2",
                    Value = 0.101971621M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
            }
        }

        public override string CategoryName => "pressure";
    }
}
