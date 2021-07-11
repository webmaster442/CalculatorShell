using CalculatorShell.Base;
using CalculatorShell.Expressions;
using CalculatorShell.Infrastructure;
using System.ComponentModel.Composition;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal sealed class Simplify : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            arguments.CheckMinimumArgumentCount(1);

            if (arguments.Count == 1)
            {
                ExecuteEpressionMode(arguments, output);
            }
            else
            {
                ExecuteMintermMode(arguments, output);
            }
        }

        private void ExecuteMintermMode(Arguments arguments, ICommandConsole output)
        {
            arguments.CheckMinimumArgumentCount(2);
            var termkind = arguments.Get<TermKind>(0, "--");
            var terms = arguments.GetRange<int>(1, arguments.Count);

            var reparsed = ExpressionFactory.ParseLogic(terms, null, new ParseLogicOptions
            {
                Culture = arguments.CurrentCulture,
                GenerateHazardFree = false,
                TermKind = termkind,
                LsbDirection = Lsb.AisMsb,
                Variables = Memory,
            });
            Memory?.SetExpression("$ans", reparsed);
            output.WriteLine("{0}", reparsed);
        }

        private void ExecuteEpressionMode(Arguments arguments, ICommandConsole output)
        {
            var expression = ParseExpression(arguments, 0);
            if (expression.IsLogicExpression())
            {
                var minterms = expression.GetMinterms();
                var reparsed = ExpressionFactory.ParseLogic(minterms, null, new ParseLogicOptions
                {
                    Culture = arguments.CurrentCulture,
                    GenerateHazardFree = false,
                    TermKind = TermKind.Minterm,
                    LsbDirection = Lsb.AisMsb,
                    Variables = Memory,
                });
                Memory?.SetExpression("$ans", reparsed);
                output.WriteLine("{0}", reparsed);
            }
            else
            {
                var simplified = expression.Simplify();
                Memory?.SetExpression("$ans", simplified);
                output.WriteLine("{0}", simplified);
            }
        }
    }
}
