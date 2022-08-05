using System.Reflection;

namespace CalculatorShell.Ui
{
    public static class TableHelper
    {
        public static string WriteTable<T>(T item)
        {
            Dictionary<string, string> items = new();
            PropertyInfo[]? properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo? property in properties)
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
            foreach (KeyValuePair<Tkey, TValue> entry in dictionary)
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
            foreach (T? item in items)
            {
                if (cols.Count > columns - 1)
                {
                    table.AddRow(cols.ToArray());
                    cols.Clear();
                }
                cols.Add(item?.ToString() ?? string.Empty);
            }
            if (cols.Count > 0)
            {
                int pad = columns - cols.Count;
                for (int i = 0; i < pad; i++)
                {
                    cols.Add(string.Empty);
                }
                table.AddRow(cols.ToArray());
            }
            return table.ToMinimalString();
        }
    }
}
