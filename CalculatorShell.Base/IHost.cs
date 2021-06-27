using System.Collections.Generic;

namespace CalculatorShell.Base
{
    public interface IHost
    {
        IEnumerable<string> Commands { get; }
        IEnumerable<string> Functions { get; }
    }
}
