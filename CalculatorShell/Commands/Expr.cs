using CalculatorShell.Base;
using CalculatorShell.Expressions;
using CalculatorShell.Infrastructure;
using CalculatorShell.Properties;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class Expr : MemoryCommandBase
    {
        protected override void PrintMemory(ICommandConsole output)
        {
            Dictionary<string, IExpression> expressions = new();
            foreach (var name in Memory!.ExpressionNames)
            {
                expressions.Add(name, Memory!.GetExpression(name));
            }
            output.WriteLine(Resources.SetExpressions);
            if (expressions.Count > 0)
                output.WriteTable<string, IExpression>(expressions);
        }

        protected override void PrintSpecificMemory(string varName, ICommandConsole output)
        {
            if (Memory!.ExpressionNames.Any(x => x == varName))
            {
                IExpression expr = Memory!.GetExpression(varName);
                if (expr != null)
                {
                    output.WriteLine(expr.ToString() ?? string.Empty);
                }
            }
            else
                throw new ExpressionEngineException(Resources.UndefinedExpression, varName);
        }

        protected override void SetMemory(string varName, string valueString, CultureInfo culture)
        {
            var parsed = ExpressionFactory.Parse(valueString, Memory!, culture);
            Memory!.SetExpression(varName, parsed);
        }
    }
}
