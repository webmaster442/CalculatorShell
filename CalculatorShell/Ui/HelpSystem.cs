using CalculatorShell.Base;
using CalculatorShell.Properties;
using System.Collections.Generic;

namespace CalculatorShell.Ui
{
    public class HelpSystem
    {
        private readonly List<string> _lines;
        private readonly Base.ConsoleColor _error;
        private readonly Base.ConsoleColor _heading;
        private readonly Base.ConsoleColor _preformat;
        private readonly Base.ConsoleColor _regular;

        public HelpSystem()
        {
            _error = new Base.ConsoleColor(0xff, 0, 0);
            _heading = new Base.ConsoleColor(0x27, 0xb3, 48);
            _regular = new Base.ConsoleColor(0xF4, 0xF5, 0xE3);
            _preformat = new Base.ConsoleColor(0x91, 0x45, 0xFF);

            var assembly = typeof(HelpSystem).Assembly;

            using var stream = assembly.GetManifestResourceStream("CalculatorShell.CmdHelp.md");
            _lines = new List<string>();
            if (stream == null)
            {
                return;
            }
            using var reader = new System.IO.StreamReader(stream);

            string? line = null;
            while ((line = reader.ReadLine()) != null)
            {
                _lines.Add(line);
            }
        }

        private (int start, int end) GetLines(string command)
        {
            int start = -1;
            int end = -1;
            for (int i=0; i<_lines.Count; i++)
            {
                if (start == -1 && _lines[i].StartsWith($"# {command}"))
                {
                    start = i;
                }
                else if (_lines[i].StartsWith($"# "))
                {
                    end = i;
                    break;
                }
            }

            if (start == -1 ||end == -1)
            {
                start = -1;
                end = -1;
            }

            return (start, end);
        }

        public void WriteGetFormattetdHelp(string command, ICommandConsole output)
        {
            var (start, end) = GetLines(command);
            if (start == -1 || end == -1)
            {
                output.CurrentFormat = new ConsoleFormat
                {
                    Foreground = _error
                };
                output.WriteLine(Resources.NoHelpFound, command);
                output.CurrentFormat = null;
                return;
            }

            bool isPreFormatted = false;
            for (int i=start; i<end; i++)
            {
                string currentline = _lines[i];
                if (currentline.StartsWith("#"))
                {
                    AppendHeading(output, currentline);
                }
                else if (currentline.StartsWith("```") && !isPreFormatted)
                {
                    isPreFormatted = true;
                    output.CurrentFormat = new ConsoleFormat
                    {
                        Foreground = _preformat
                    };
                }
                else if (currentline.StartsWith("```") && isPreFormatted)
                {
                    isPreFormatted = false;
                    output.CurrentFormat = null;
                }
                else if (isPreFormatted)
                {
                    output.WriteLine(currentline);
                }
                else
                {
                    AppendRegularText(output, currentline);
                }
            }
        }

        private void AppendRegularText(ICommandConsole output, string currentline)
        {
            output.CurrentFormat = new ConsoleFormat
            {
                Foreground = _regular
            };
            output.WriteLine(currentline);
            output.CurrentFormat = null;
        }

        private void AppendHeading(ICommandConsole output, string currentline)
        {
            output.CurrentFormat = new ConsoleFormat
            {
                Foreground = _heading
            };
            output.WriteLine(currentline);
            output.CurrentFormat = null;
        }
    }
}
