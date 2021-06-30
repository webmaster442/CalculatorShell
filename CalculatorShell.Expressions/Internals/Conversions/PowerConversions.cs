using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorShell.Expressions.Internals.Conversions
{
    internal sealed class PowerConversions : ConstantConversionBase
    {
        public PowerConversions(CultureInfo culture) : base(culture)
        {
        }

        public override IEnumerable<ConstantBasedConversion> UnitConstants
        {
            get
            {
                yield return new ConstantBasedConversion
                {
                    UnitName = "watt",
                    Value = 1
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "horsepower",
                    Value = 735.49875M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase,
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "btu/s",
                    Value = 1055.05585262M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase,
                };
            }
        }

        public override string CategoryName => "Power";
    }
}
