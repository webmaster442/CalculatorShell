using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorShell.Infrastructure
{
    internal class CommandRunner
    {
        private static IEnumerable<string> ParseArguments(string commandLine)
        {
            if (string.IsNullOrWhiteSpace(commandLine))
                yield break;

            var buffer = new StringBuilder(1024);
            bool inQuote = false;
            foreach (char c in commandLine)
            {
                if (c == '"' && !inQuote)
                {
                    inQuote = true;
                    continue;
                }

                if (c != '"' && !(char.IsWhiteSpace(c) && !inQuote))
                {
                    buffer.Append(c);
                    continue;
                }

                if (buffer.Length > 0)
                {
                    var result = buffer.ToString();
                    buffer.Clear();
                    inQuote = false;
                    yield return result;
                }
            }

            if (buffer.Length > 0)
                yield return buffer.ToString();
        }
    }
}
