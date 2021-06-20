﻿using CalculatorShell.Infrastructure;
using System.ComponentModel.Composition;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal sealed class Clear : CommandBase
    {
        public override void Execute(string[] arguments, ICommandConsole output)
        {
            output.Clear();
        }
    }
}
