using CalculatorShell.Base;
using CalculatorShell.Expressions;
using CalculatorShell.Infrastructure;
using System;
using System.ComponentModel.Composition;


namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class Expr : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            if (Memory == null)
                throw new InvalidOperationException();

            arguments.CheckArgumentCount(2);

            var name = arguments.Get<string>(0);

            var parsed =  ExpressionFactory.Parse(arguments.Get<string>(1), Memory, arguments.CurrentCulture);

            Memory.SetExpression(name, parsed);
        }
    }
}
