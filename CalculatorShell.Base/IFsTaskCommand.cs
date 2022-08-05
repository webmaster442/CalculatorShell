namespace CalculatorShell.Base
{
    /// <summary>
    /// Base interface for a command that runs on a sepperate thread from the base shell and
    /// has acces to the host machine file system
    /// </summary>
    public interface IFsTaskCommand : ICommand
    {
        /// <summary>
        /// Main entry point for the command
        /// </summary>
        /// <param name="arguments">Arguments passed on to the command</param>
        /// <param name="output">Console output</param>
        /// <param name="cancellationToken">Cancelation token</param>
        /// <param name="fs">File sysem acces</param>
        /// <returns>an awaitable task</returns>
        Task Execute(Arguments arguments, ICommandConsole output, IFsHost fs, CancellationToken cancellationToken);
    }
}
