using System.Globalization;

namespace CalculatorShell.Expressions
{
    /// <summary>
    /// Represents know term kinds
    /// </summary>
    public enum TermKind
    {
        /// <summary>
        /// Interpret given terms as minterms
        /// </summary>
        Minterm,
        /// <summary>
        /// Interpret given terms as maxterms
        /// </summary>
        Maxterm
    }

    /// <summary>
    /// LSB directrion
    /// </summary>
    public enum Lsb
    {
        /// <summary>
        /// variable a will be interpreted as LSB
        /// </summary>
        AisLsb,
        /// <summary>
        /// variable a will be interpreted as MSB
        /// </summary>
        AisMsb,
    }

    /// <summary>
    /// Logic parser options
    /// </summary>
    public class ParseLogicOptions
    {
        /// <summary>
        /// Specifies the term kind
        /// </summary>
        public TermKind TermKind { get; init; }
        /// <summary>
        /// Specifies the least significant bit interpretaton
        /// </summary>
        public Lsb LsbDirection { get; init; }
        /// <summary>
        /// Generate Static Hazard free version of the logic expression
        /// </summary>
        public bool GenerateHazardFree { get; init; }

        /// <summary>
        /// IVariables reference for expression parsing and evaluation
        /// </summary>
        public IVariables? Variables { get; init; }

        /// <summary>
        /// Expression parsing culture
        /// </summary>
        public CultureInfo Culture { get; init; } = CultureInfo.InvariantCulture;
    }
}
