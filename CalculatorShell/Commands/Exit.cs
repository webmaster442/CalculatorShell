namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal sealed class Exit : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            arguments.CheckArgumentCount(0);

            if (Host != null)
            {
                Host.Shutdown();
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
