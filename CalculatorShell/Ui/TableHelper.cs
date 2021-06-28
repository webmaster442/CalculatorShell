using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CalculatorShell.Ui
{
    public static class TableHelper
    {
        public static string WriteTable<T>(T item)
        {
            Dictionary<string, string> items = new();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                items.Add(property.Name, property.GetValue(item)?.ToString() ?? string.Empty);
            }
            return WriteTable<string, string>(items);
        }


        public static string WriteTable<Tkey, TValue>(IDictionary<Tkey, TValue> dictionary)
        {
            ConsoleTable table = new(new ConsoleTableOptions
            {
                Columns = new string[] { "Key/Property:", "Value" },
                NumberAlignment = CellAlignment.Left,
                EnableCount = false,
            });
            foreach (var entry in dictionary)
            {
                table.AddRow(entry.Key, entry.Value);
            }
            return table.ToMinimalString();
        }

        public static string WriteTable<T>(IEnumerable<T> items, int columns = 4)
        {
            ConsoleTable table = new(new ConsoleTableOptions
            {
                Columns = Enumerable.Repeat(string.Empty, columns),
                NumberAlignment = CellAlignment.Left,
                EnableCount = false,
            });
            List<string> cols = new(columns);
            foreach (var item in items)
            {

                if (cols.Count < columns)
                {
                    cols.Add(item?.ToString() ?? string.Empty);
                }
                else
                {
                    table.AddRow(cols.ToArray());
                    cols.Clear();
                }

            }
            return table.ToMinimalString();
        }
    }
}
