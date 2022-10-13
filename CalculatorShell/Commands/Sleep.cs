namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal sealed class Sleep : CommandBase, ITaskCommand
    {
        public async Task Execute(Arguments arguments, ICommandConsole output, CancellationToken cancellationToken)
        {
            arguments.CheckArgumentCount(1);

            uint seconds = arguments.Get<uint>(0);
            for (uint i = 0; i < seconds; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                float progress = (float)i / seconds;
                output.Report(progress);
                await Task.Delay(1000);
            }
        }
    }
}
