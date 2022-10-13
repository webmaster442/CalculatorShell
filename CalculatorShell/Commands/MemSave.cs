namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class MemSave : MemorySerializeCommandBase, IFsTaskCommand
    {
        public async Task Execute(Arguments arguments, ICommandConsole output, IFsHost fs, CancellationToken cancellationToken)
        {
            arguments.CheckArgumentCount(1);
            string fileName = arguments.Get<string>(0);

            using (Stream? fss = fs.CreateOrOverwrite(fileName))
            {
                await Serialize(fss, cancellationToken);
            }
        }
    }
}