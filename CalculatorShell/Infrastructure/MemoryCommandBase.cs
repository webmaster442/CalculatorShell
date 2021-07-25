using CalculatorShell.Base;
using System;
using System.Globalization;

namespace CalculatorShell.Infrastructure
{
    internal abstract class MemoryCommandBase : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            if (Memory == null)
                throw new InvalidOperationException();

            arguments.CheckArgumentCount(0, 1, 2);

            switch (arguments.Count)
            {
                case 0:
                    PrintMemory(output);
                    break;
                case 1:
                    PrintSpecificMemory(arguments.Get<string>(0), output);
                    break;
                case 2:
                    SetMemory(arguments.Get<string>(0), arguments.Get<string>(1), arguments.CurrentCulture);
                    break;

            }
        }

        protected abstract void SetMemory(string varName, string valueString, CultureInfo culture);

        protected abstract void PrintSpecificMemory(string varName, ICommandConsole output);

        protected abstract void PrintMemory(ICommandConsole output);
    }
}
