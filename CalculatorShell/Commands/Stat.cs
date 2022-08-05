using CalculatorShell.Base;
using CalculatorShell.Expressions;
using CalculatorShell.Infrastructure;
using CalculatorShell.Maths;
using System.ComponentModel.Composition;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class Stat : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            if (Memory == null)
                throw new InvalidOperationException();

            arguments.CheckMinimumArgumentCount(1);

            double[]? parameters = arguments.GetRange<double>().ToArray();
            StatisticResult? result = Statistics.Calculate(parameters);
            output.WriteTable(result);

            Memory["ans"] = result.CreateNumber();
        }
    }
}
