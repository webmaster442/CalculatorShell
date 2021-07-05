using CalculatorShell.Base;
using System;
using System.Collections.Generic;
using System.IO;

namespace CalculatorShell.Infrastructure
{
    internal class FsHost : IFsHost
    {
        private string _currentDirectory;

        public FsHost()
        {
            _currentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
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

        public IEnumerable<string> GetDirectories()
        {
            return Directory.GetDirectories(CurrentDirectory);
        }

        public IEnumerable<string> GetFiles()
        {
            return Directory.GetFiles(CurrentDirectory);
        }
    }
}
