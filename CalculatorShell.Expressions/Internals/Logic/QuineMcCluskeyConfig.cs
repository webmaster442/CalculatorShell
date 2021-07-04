namespace CalculatorShell.Expressions.Internals.Logic
{
    internal class QuineMcCluskeyConfig
    {
        /// <summary>
        /// If set, returns hazard free version of the expression
        /// </summary>
        public bool HazardFree { get; set; }

        /// <summary>
        /// If set, A variable is treated as the least significant
        /// </summary>
        public bool AIsLsb { get; set; }

        /// <summary>
        /// Negate the result expresion or not
        /// </summary>
        public bool Negate { get; set; }
    }
}
