using CalculatorShell.Base;
using CalculatorShell.Infrastructure;
using CalculatorShell.Properties;
using System;
using System.ComponentModel.Composition;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class Date : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            arguments.CheckArgumentCount(0);
            output.WriteLine(Resources.DateFormat, GetDate(DateTime.Now.Date), DateTime.Now.DayOfYear, DateTime.Now.DayOfWeek);
        }

        private static string GetDate(DateTime date)
        {
            return $"{date.Year}/{date.Month}/{date.Day}";
        }
    }
}
