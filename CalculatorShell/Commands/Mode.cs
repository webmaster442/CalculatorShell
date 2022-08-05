using CalculatorShell.Base;
using CalculatorShell.Expressions;
using CalculatorShell.Infrastructure;
using CalculatorShell.Maths;
using CalculatorShell.Properties;
using System.ComponentModel.Composition;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class Mode : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            arguments.CheckArgumentCount(1);

            string? mode = arguments.Get<string>(0);
            if (Enum.TryParse<AngleMode>(mode, true, out AngleMode parsed))
            {
                ExpressionFactory.CurrentAngleMode = parsed;
            }
            else
            {
                throw new ExpressionEngineException(Resources.UnknownMode, mode);
            }
        }
    }
}
