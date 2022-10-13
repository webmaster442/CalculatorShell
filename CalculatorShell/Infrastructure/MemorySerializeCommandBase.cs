using CalculatorShell.Base.Domain;
using CalculatorShell.Properties;
using System.Globalization;
using System.Text.Json;

namespace CalculatorShell.Infrastructure
{
    internal abstract class MemorySerializeCommandBase : CommandBase
    {
        private static JsonSerializerOptions CreateOptions()
        {
            return new JsonSerializerOptions
            {
                IncludeFields = false,
                WriteIndented = true,
            };
        }

        protected async ValueTask Deserialize(Stream source, CancellationToken token)
        {

            MemoryData? data = await JsonSerializer.DeserializeAsync<MemoryData>(source, CreateOptions(), token);

            if (data == null)
                throw new InvalidOperationException(Resources.FileLoadError);

            if (Memory == null)
                throw new InvalidOperationException();

            Memory.Clear();
            Memory.ClearExpressions();

            foreach (KeyValuePair<string, string> variable in data.Variables)
            {
                Memory[variable.Key] = NumberSerializerFactory.Deserialize(variable.Value);
            }

            foreach (KeyValuePair<string, string> expression in data.Expressions)
            {
                IExpression? parsed = ExpressionFactory.Parse(expression.Value, Memory, CultureInfo.InvariantCulture);
                Memory.SetExpression(expression.Key, parsed);
            }
        }

        protected async ValueTask Serialize(Stream target, CancellationToken token)
        {
            if (Memory == null)
                throw new InvalidOperationException();

            MemoryData result = new();
            foreach (string? name in Memory.VariableNames)
            {
                result.Variables[name] = NumberSerializerFactory.Serialize(Memory[name]);
            }

            foreach (string? name in Memory.ExpressionNames)
            {
                string? value = Memory.GetExpression(name).ToString(CultureInfo.InvariantCulture);
                result.Expressions[name] = value;
            }

            await JsonSerializer.SerializeAsync(target, result, CreateOptions(), token);
        }
    }
}
