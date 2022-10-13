namespace CalculatorShell.Infrastructure
{
    internal class FsHost : IFsHost
    {
        private string _currentDirectory;

        public FsHost()
        {
            _currentDirectory = Home;
        }

        public string CurrentDirectory
        {
            get { return _currentDirectory; }
            set
            {
                string parsed = Environment.ExpandEnvironmentVariables(value);
                if (Directory.Exists(parsed))
                {
                    _currentDirectory = parsed;
                }
                else
                    throw new InvalidOperationException("Path doesn't exist");
            }
        }

        public string Home => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        public Stream CreateOrOverwrite(string name)
        {
            string? full = Path.Combine(CurrentDirectory, name);
            return File.Create(full);
        }

        public bool FileExists(string name)
        {
            string? full = Path.Combine(CurrentDirectory, name);
            return File.Exists(full);
        }

        public IEnumerable<string> GetDirectories()
        {
            return Directory.GetDirectories(CurrentDirectory);
        }

        public IEnumerable<string> GetFiles()
        {
            return Directory.GetFiles(CurrentDirectory);
        }

        public Stream OpenRead(string name)
        {
            string? full = Path.Combine(CurrentDirectory, name);
            return File.OpenRead(full);
        }
    }
}
