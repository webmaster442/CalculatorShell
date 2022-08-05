namespace CalculatorShell.Base
{
    /// <summary>
    /// Base interface for a command that runs on a sepperate thread from the base shell.
    /// USe this interface, if your command requires a long time (more than 2 seconds) to execute
    /// </summary>
    public interface ITaskCommand : ICommand
    {
        /// <summary>
        /// Main entry point for the command
        /// </summary>
        /// <param name="arguments">Arguments passed on to the command</param>
        /// <param name="output">Console output</param>
        /// <param name="cancellationToken">Cancelation token</param>
        /// <returns>an awaitable task</returns>
        Task Execute(Arguments arguments, ICommandConsole output, CancellationToken cancellationToken);
    }
}
