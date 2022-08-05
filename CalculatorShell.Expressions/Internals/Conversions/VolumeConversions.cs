using System.Globalization;

namespace CalculatorShell.Expressions.Internals.Conversions
{
    internal class VolumeConversions : ConstantConversionBase
    {
        public VolumeConversions(CultureInfo culture) : base(culture)
        {
        }

        public override IEnumerable<ConstantBasedConversion> UnitConstants
        {
            get
            {
                yield return new ConstantBasedConversion
                {
                    UnitName = "Litres",
                    Value = 1M
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Inches3",
                    Value = 61.02374409M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Feet3",
                    Value = 0.035314667M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Yards3",
                    Value = 0.001307951M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Cups",
                    Value = 4.226752838M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Gallons",
                    Value = 0.219969152M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Meters3",
                    Value = 0.001M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Ounces",
                    Value = 35.19506424M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Pints",
                    Value = 2.113376419M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Quarts",
                    Value = 1.056688209M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Tablespoons",
                    Value = 67.6280454M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Teaspoons",
                    Value = 202.8841362M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
            }
        }

        public override string CategoryName => "volume";
    }
}
