using System.Collections.Generic;

namespace CalculatorShell.Base
{
    /// <summary>
    /// API that has to be provided by the command host to the commands
    /// </summary>
    public interface IHost
    {
        /// <summary>
        /// Returns true, if the program can run.
        /// </summary>
        public bool CanRun { get; }

        /// <summary>
        /// Currently defined commands that the command host understands
        /// </summary>
        IEnumerable<string> Commands { get; }
        /// <summary>
        /// Currently defined variables that the expresssion parser understands
        /// </summary>
        IEnumerable<string> Functions { get; }

        /// <summary>
        /// Provides acces to storage
        /// </summary>
        IStorage Storage { get; }

        /// <summary>
        /// Request app shutdown
        /// </summary>
        void Shutdown();
    }

    /// <summary>
    /// Extended host inteface
    /// </summary>
    public interface IHostEx : IHost
    {
        /// <summary>
        /// Set commands
        /// </summary>
        /// <param name="commands">commands</param>
        void SetCommands(IEnumerable<string> commands);
        /// <summary>
        /// Set functions
        /// </summary>
        /// <param name="functions">functions</param>
        void SetFunctions(IEnumerable<string> functions);
    }
}
