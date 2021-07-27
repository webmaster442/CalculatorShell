using CalculatorShell.Base;
using CalculatorShell.Properties;
using System;
using System.Collections.Generic;

namespace CalculatorShell.Ui
{
    public class TextViewer
    {
        private readonly List<string> _lines;
        private readonly Base.ConsoleColor _error;
        private readonly Base.ConsoleColor _heading;
        private readonly Base.ConsoleColor _preformat;
        private readonly Base.ConsoleColor _regular;
        private readonly ICommandConsole _output;

        public TextViewer(ICommandConsole output, string manifestResourceName)
        {
            _output = output;
            _error = new Base.ConsoleColor(0xff, 0, 0);
            _heading = new Base.ConsoleColor(0x27, 0xb3, 48);
            _regular = new Base.ConsoleColor(0xF4, 0xF5, 0xE3);
            _preformat = new Base.ConsoleColor(0x91, 0x45, 0xFF);

            var assembly = typeof(TextViewer).Assembly;

            using var stream = assembly.GetManifestResourceStream(manifestResourceName);
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
            for (int i = 0; i < _lines.Count; i++)
            {
                if (start == -1
                    && _lines[i].StartsWith("# ")
                    && _lines[i].Contains(command))
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

        private void AppendRegularText(string currentline)
        {
            _output.CurrentFormat = new ConsoleFormat
            {
                Foreground = _regular
            };
            _output.WriteLine(currentline);
            _output.CurrentFormat = null;
        }

        private void AppendHeading(string currentline)
        {
            _output.CurrentFormat = new ConsoleFormat
            {
                Foreground = _heading
            };
            _output.WriteLine(currentline);
            _output.CurrentFormat = null;
        }

        private void WriteLines(int start, int end, int screenMax)
        {
            int written = 0;
            bool isPreFormatted = false;
            for (int i = start; i < end; i++)
            {
                string currentline = _lines[i];
                if (currentline.StartsWith("#"))
                {
                    AppendHeading(currentline);
                }
                else if (currentline.StartsWith("```") && !isPreFormatted)
                {
                    isPreFormatted = true;
                    _output.CurrentFormat = new ConsoleFormat
                    {
                        Foreground = _preformat
                    };
                }
                else if (currentline.StartsWith("```") && isPreFormatted)
                {
                    isPreFormatted = false;
                    _output.CurrentFormat = null;
                }
                else if (isPreFormatted)
                {
                    _output.WriteLine(currentline);

                }
                else
                {
                    AppendRegularText(currentline);
                }
                written += currentline.Length + 1;
                if (written > screenMax)
                {
                    _output.WriteLine("\nPress a key to contine");
                    var key = _output.ReadKey();
                    if (key == ConsoleKey.Escape)
                    {
                        break;
                    }
                    Console.Clear();
                    written = 0;
                }
            }
        }

        public void Show(string? chapter = null)
        {
            BufferSwap(true);
            int screenMax = (Console.WindowHeight - 2) * Console.WindowWidth;

            (int start, int end) pos = (0, _lines.Count);

            if (!string.IsNullOrEmpty(chapter))
            {
                pos = GetLines(chapter);
                if (pos.start == -1 || pos.end == -1)
                {
                    _output.CurrentFormat = new ConsoleFormat
                    {
                        Foreground = _error
                    };
                    _output.WriteLine(Resources.NoHelpFound, chapter);
                    _output.CurrentFormat = null;
                    return;
                }
            }

            WriteLines(pos.start, pos.end, screenMax);
            _output.WriteLine("\nPress a key to exit");
            _output.ReadKey();

            BufferSwap(false);
        }
    }
}
