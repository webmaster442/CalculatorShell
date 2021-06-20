namespace CalculatorShell.Base
{
    public interface ISimpleCommand : ICommand
    {
        void Execute(Arguments arguments, ICommandConsole output);
    }
}
