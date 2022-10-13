namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class Differentiate : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            if (Memory == null)
                throw new InvalidOperationException();

            arguments.CheckArgumentCount(2);

            string expression = arguments.Get<string>(0);
            string byVariable = arguments.Get<string>(1);

            IExpression? exp;

            if (expression.StartsWith('$'))
            {
                exp = Memory.GetExpression(expression);
            }
            else
            {
                exp = ExpressionFactory.Parse(expression, Memory, arguments.CurrentCulture);
            }

            exp = exp.Differentiate(byVariable).Simplify();

            Memory.SetExpression("$ans", exp);
            output.WriteLine("{0}", exp);
        }
    }
}
