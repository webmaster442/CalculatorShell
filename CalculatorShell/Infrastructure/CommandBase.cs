﻿namespace CalculatorShell.Infrastructure
{
    internal abstract class CommandBase : ICommand
    {
        [Import(typeof(IMemory))]
        public IMemory? Memory { get; set; }

        [Import(typeof(IHost))]
        public IHost? Host { get; set; }

        public virtual IEnumerable<string> Aliases => Enumerable.Empty<string>();

        protected IExpression ParseExpression(Arguments arguments, int index)
        {
            if (Memory == null)
                throw new InvalidOperationException();

            string? expressionString = arguments.Get<string>(index);

            if (expressionString.StartsWith('$'))
                return Memory.GetExpression(expressionString);
            else
                return ExpressionFactory.Parse(expressionString, Memory, arguments.CurrentCulture);
        }
    }
}
