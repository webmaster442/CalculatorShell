namespace CalculatorShell.Base
{
    public interface ICommand
    {
        IHost? Host { get; set; }
        IMemory? Memory { get; set; }
    }
}
