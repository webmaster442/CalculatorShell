using System.Collections.Generic;

namespace CalculatorShell.Base
{
    /// <summary>
    /// API that has to be provided by the command host to the commands
    /// </summary>
    public interface IHost
    {
        /// <summary>
        /// Currently defined commands that the command host understands
        /// </summary>
        IEnumerable<string> Commands { get; }
        /// <summary>
        /// Currently defined variables that the expresssion parser understands
        /// </summary>
        IEnumerable<string> Functions { get; }
    }
}
