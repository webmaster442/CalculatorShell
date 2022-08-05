namespace CalculatorShell.Expressions.Internals.Conversions
{
    internal class FlowConversions : ConstantConversionBase
    {
        public FlowConversions(System.Globalization.CultureInfo culture) : base(culture)
        {
        }

        public override IEnumerable<ConstantBasedConversion> UnitConstants
        {
            get
            {
                yield return new ConstantBasedConversion
                {
                    Value = 1,
                    UnitName = "Liters/second",
                };
                yield return new ConstantBasedConversion
                {
                    Value = 60,
                    UnitName = "Liters/minute",
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase,
                };
                yield return new ConstantBasedConversion
                {
                    Value = 3600,
                    UnitName = "Litres/hour",
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase,
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Gallons/hour",
                    Value = 951.0193885M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Gallons/minute",
                    Value = 15.85032314M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Gallons/second",
                    Value = 0.264172052M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Meters3/hour",
                    Value = 39878M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Meters3/minute",
                    Value = 0.06M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
                yield return new ConstantBasedConversion
                {
                    UnitName = "Meters3/second",
                    Value = 0.001M,
                    BaseRelationShip = MultipleOfBase,
                    TargetRelationShip = DivideBase
                };
            }
        }

        public override string CategoryName => "flow";
    }
}
/*

 */