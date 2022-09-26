using CalculatorShell.Base;
using CalculatorShell.Properties;

namespace CalculatorShell.Ui
{
    public sealed class TextViewer
    {
        private readonly List<string> _lines;
        private readonly Base.ConsoleColor _error;
        private readonly Base.ConsoleColor _heading;
        private readonly Base.ConsoleColor _preformat;
        private readonly Base.ConsoleColor _regular;

        public TextViewer()
        {
            _error = new Base.ConsoleColor(0xff, 0, 0);
            _heading = new Base.ConsoleColor(0x27, 0xb3, 48);
            _regular = new Base.ConsoleColor(0xF4, 0xF5, 0xE3);
            _preformat = new Base.ConsoleColor(0x91, 0x45, 0xFF);
            _lines = new List<string>();
        }

        public void LoadText(string manifestResourceName)
        {
            System.Reflection.Assembly? assembly = typeof(TextViewer).Assembly;

            using Stream? stream = assembly.GetManifestResourceStream(manifestResourceName);
            if (stream == null)
            {
                return;
            }
            using StreamReader? reader = new System.IO.StreamReader(stream);

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
            for (int i = 0; i < _lines.Count; i++)
            {
                if (start == -1
                    && _lines[i].StartsWith($"# {command}"))
                {
                    start = i;
                }
                else if (start != -1
                    && _lines[i].StartsWith("# "))
                {
                    end = i;
                    break;
                }
            }

            if (end == -1)
                end = _lines.Count;

            if (start == -1)
            {
                start = -1;
                end = -1;
            }

            return (start, end);
        }

        private static void BufferSwap(bool alternate)
        {
            if (alternate)
                Console.Write("\x1b[?1049h");
            else
                Console.Write("\x1b[?1049l");
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

        private bool WriteLines(ICommandConsole output, int start, int end, int maxRowCount)
        {
            int written = 0;
            bool isPreFormatted = false;
            for (int i = start; i < end; i++)
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
                ++written;
                if (written > maxRowCount)
                {
                    output.WriteLine("\nPress a key to contine or ESC to exit...");
                    ConsoleKey key = output.ReadKey();
                    if (key == ConsoleKey.Escape)
                    {
                        return false;
                    }
                    Console.Clear();
                    written = 0;
                }
            }
            return true;
        }

        public void Show(ICommandConsole output, string? chapter = null)
        {
            BufferSwap(alternate: true);
            int screenRows = Console.WindowHeight - 2;

            (int start, int end) pos = (0, _lines.Count);

            if (!string.IsNullOrEmpty(chapter))
            {
                pos = GetLines(chapter);
                if (pos.start == -1 || pos.end == -1)
                {
                    output.CurrentFormat = new ConsoleFormat
                    {
                        Foreground = _error
                    };
                    output.WriteLine(Resources.NoHelpFound, chapter);
                    output.CurrentFormat = null;
                    return;
                }
            }

            if (WriteLines(output, pos.start, pos.end, screenRows))
            {
                output.WriteLine("\nPress a key to exit...");
                output.ReadKey();
            }

            BufferSwap(alternate: false);
        }
    }
}
