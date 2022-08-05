namespace CalculatorShell.Ui
{
    public sealed class ConsoleTableOptions
    {
        public ConsoleTableOptions()
        {
            Columns = Enumerable.Empty<string>();
            EnableCount = true;
            NumberAlignment = CellAlignment.Left;
            OutputTo = Console.Out;
        }

        public IEnumerable<string> Columns { get; set; }

        public bool EnableCount { get; set; }

        /// <summary>
        /// Enable only from a list of objects
        /// </summary>
        public CellAlignment NumberAlignment { get; set; }

        /// <summary>
        /// The <see cref="TextWriter"/> to write to. Defaults to <see cref="Console.Out"/>.
        /// </summary>
        public TextWriter OutputTo { get; set; }
    }
}
