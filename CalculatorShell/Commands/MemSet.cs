using CalculatorShell.Base;
using CalculatorShell.Expressions;
using CalculatorShell.Infrastructure;
using System;
using System.ComponentModel.Composition;

namespace CalculatorShell.Commands
{

    [Export(typeof(ICommand))]
    internal class MemSet : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            if (Memory != null)
            {
                var name = arguments.Get<string>(0);
                var strExpression = arguments.Get<string>(1);
                var value = ExpressionFactory.Parse(strExpression, Memory, arguments.CurrentCulture)?.Evaluate();
                if (value == null)
                    throw new InvalidOperationException();
                Memory[name] = value;
            }
        }
    }
}
