﻿namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class Version : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            if (Host == null)
                throw new InvalidOperationException();

            output.WriteLine("{0}", Host.HostVersion);
        }
    }
}
