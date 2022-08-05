using System.Globalization;

namespace CalculatorShell.Expressions.Internals.Conversions
{
    internal sealed class LengthConversions : ConstantConversionBase
    {
        public LengthConversions(CultureInfo culture) : base(culture)
        {
        }

        public override string CategoryName => "Length";

        public override IEnumerable<ConstantBasedConversion> UnitConstants
        {
            get
            {
                yield return new ConstantBasedConversion
                {
                    Value = 1,
                    UnitName = "meter",
                };
                yield return new ConstantBasedConversion
                {
                    Value = 0.3048M, //m -> foot
                    UnitName = "foot",
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    Value = 0.0254M,
                    UnitName = "inch",
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    Value = 1609.344M,
                    UnitName = "mile",
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    Value = 0.9144M,
                    UnitName = "yard",
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
            }
        }
    }
}
