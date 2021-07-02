using CalculatorShell.Base;
using CalculatorShell.Expressions;
using CalculatorShell.Infrastructure;
using CalculatorShell.Properties;
using System;
using System.Collections.Generic;
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

            arguments.CheckArgumentCount(0, 2);

            if (arguments.Count == 0)
            {
                Dictionary<string, IExpression> expressions = new();
                foreach (var name in Memory.ExpressionNames)
                {
                    expressions.Add(name, Memory.GetExpression(name));
                }
                output.WriteLine(Resources.SetExpressions);
                if (expressions.Count > 0)
                    output.WriteTable<string, IExpression>(expressions);
            }
            else
            {
                var name = arguments.Get<string>(0);

                var parsed = ExpressionFactory.Parse(arguments.Get<string>(1), Memory, arguments.CurrentCulture);

                Memory.SetExpression(name, parsed);
            }
        }
    }
}
