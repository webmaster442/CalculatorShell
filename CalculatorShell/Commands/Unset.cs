namespace CalculatorShell.Commands
{

    [Export(typeof(ICommand))]
    internal class Unset : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            if (Memory == null)
                throw new InvalidOperationException();

            arguments.CheckArgumentCount(0, 1);

            if (arguments.Count == 1)
            {
                string? name = arguments.Get<string>(0);
                Memory.Delete(name);
            }
            else
            {
                Memory.Clear();
            }
        }
    }
}
