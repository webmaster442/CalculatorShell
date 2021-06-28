namespace CalculatorShell.Ui
{
    public interface IAutoCompleteHandler
    {
        char[] Separators { get; }
        string[] GetSuggestions(string text, int index);
    }
}