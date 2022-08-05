using System.Globalization;

namespace CalculatorShell.Expressions.Internals.Conversions
{
    internal class AreaConversions : ConstantConversionBase
    {
        public AreaConversions(CultureInfo culture) : base(culture)
        {
        }

        public override IEnumerable<ConstantBasedConversion> UnitConstants
        {
            get
            {
                yield return new ConstantBasedConversion
                {
                    UnitName = "Meters2",
                    Value = 1M
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Acres",
                    Value = 0.000247104M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Feet2",
                    Value = 10.76391042M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Hectares",
                    Value = 0.0001M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Inches2",
                    Value = 1550.0031M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Miles2",
                    Value = 3.86E-07M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Yards2",
                    Value = 1.195990046M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
            }
        }

        public override string CategoryName => "area";
    }
}
