namespace CalculatorShell.Base
{
    /// <summary>
    /// Base interface for a command that runs on the same thread as the base shell
    /// </summary>
    public interface ISimpleCommand : ICommand
    {
        /// <summary>
        /// Main entry point for the command.
        /// </summary>
        /// <param name="arguments">Arguments passed on to the command</param>
        /// <param name="output">Console output</param>
        void Execute(Arguments arguments, ICommandConsole output);
    }
}
