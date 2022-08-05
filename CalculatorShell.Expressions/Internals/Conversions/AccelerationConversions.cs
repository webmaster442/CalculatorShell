using System.Globalization;

namespace CalculatorShell.Expressions.Internals.Conversions
{
    internal class AccelerationConversions : ConstantConversionBase
    {
        public AccelerationConversions(CultureInfo culture) : base(culture)
        {
        }

        public override IEnumerable<ConstantBasedConversion> UnitConstants
        {
            get
            {
                yield return new ConstantBasedConversion
                {
                    UnitName = "Meters/sec2",
                    Value = 1M
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Feet/sec2",
                    Value = 3.280839895M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Gravity",
                    Value = 0.101971621M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Inches/sec2",
                    Value = 39.37007874M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
            }
        }

        public override string CategoryName => "acceleration";
    }
}