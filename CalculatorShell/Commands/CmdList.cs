namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal sealed class CmdList : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            arguments.CheckArgumentCount(0);

            if (Host == null)
                throw new InvalidOperationException();

            output.WriteLine("Known commands:");
            output.WriteTable(Host.Commands.OrderBy(x => x), 6);
            output.WriteLine("Known functions:");
            output.WriteTable(Host.Functions.OrderBy(x => x), 6);
        }
    }
}
