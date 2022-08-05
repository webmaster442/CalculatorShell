namespace CalculatorShell.Base.Domain
{

    /// <summary>
    /// Memory data for serialization and deserialization
    /// </summary>
    public record MemoryData
    {
        /// <summary>
        /// Expressions
        /// </summary>
        public Dictionary<string, string> Expressions { get; init; }

        /// <summary>
        /// Variables
        /// </summary>
        public Dictionary<string, string> Variables { get; init; }

        /// <summary>
        /// Creates a new instance of MemoryData
        /// </summary>
        public MemoryData()
        {
            Expressions = new Dictionary<string, string>();
            Variables = new Dictionary<string, string>();
        }
    }
}
