namespace CalculatorShell.Base
{
    /// <summary>
    /// Base interface for a shell command
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Provides acces to the command host API
        /// </summary>
        IHost? Host { get; set; }
        /// <summary>
        /// Provides API for memory (variable) management
        /// </summary>
        IMemory? Memory { get; set; }
    }
}
