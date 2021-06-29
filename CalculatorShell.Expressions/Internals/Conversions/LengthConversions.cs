using System.Collections.Generic;
using System.Globalization;

namespace CalculatorShell.Expressions.Internals.Conversions
{
    internal class LengthConversions : ConstantConversionBase
    {
        public LengthConversions(CultureInfo culture) : base(culture)
        {
        }

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
                    Value = 0.3048M,
                    UnitName = "foot",
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
            }
        }
    }
}
