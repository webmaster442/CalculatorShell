namespace CalculatorShell.ReadLine
{
    public interface IAutoCompleteHandler
    {
        char[] Separators { get; }
        string[] GetSuggestions(string text, int index);
    }
}