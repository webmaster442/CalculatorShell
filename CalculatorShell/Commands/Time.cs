namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class Time : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            arguments.CheckArgumentCount(0);
            output.WriteLine("{0}:{1}", DateTime.Now.Hour, DateTime.Now.Minute);
        }
    }
}
