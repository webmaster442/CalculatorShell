using CalculatorShell.Base;
using CalculatorShell.Expressions;
using CalculatorShell.Infrastructure;
using CalculatorShell.Properties;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class Mem : MemoryCommandBase
    {
        protected override void PrintMemory(ICommandConsole output)
        {
            Dictionary<string, string> vars = new();
            foreach (var name in Memory!.VariableNames)
            {
                vars.Add(name, Memory[name].ToString());
            }
            output.WriteLine(Resources.SetVariables);
            if (vars.Count > 0)
                output.WriteTable<string, string>(vars);
        }

        protected override void PrintSpecificMemory(string varName, ICommandConsole output)
        {
            if (Memory!.IsVariable(varName))
                output.WriteLine("{0}", Memory![varName]);
            else
                throw new ExpressionEngineException(Resources.UndefinedVariable, varName);
        }

        protected override void SetMemory(string varName, string valueString, CultureInfo culture)
        {
            Memory![varName] = ExpressionFactory.Parse(valueString, Memory, culture).Evaluate();
        }
    }
}
