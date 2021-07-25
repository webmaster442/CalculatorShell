using CalculatorShell.Base.Domain;
using CalculatorShell.Expressions;
using CalculatorShell.Properties;
using System;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

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

            var data = await JsonSerializer.DeserializeAsync<MemoryData>(source, CreateOptions(), token);

            if (data == null)
                throw new InvalidOperationException(Resources.FileLoadError);

            if (Memory == null)
                throw new InvalidOperationException();

            Memory.Clear();
            Memory.ClearExpressions();

            foreach (var variable in data.Variables)
            {
                Memory[variable.Key] = NumberSerializerFactory.Deserialize(variable.Value);
            }

            foreach (var expression in data.Expressions)
            {
                var parsed = ExpressionFactory.Parse(expression.Value, Memory, CultureInfo.InvariantCulture);
                Memory.SetExpression(expression.Key, parsed);
            }
        }

        protected async ValueTask Serialize(Stream target, CancellationToken token)
        {
            if (Memory == null)
                throw new InvalidOperationException();

            MemoryData result = new();
            foreach (var name in Memory.VariableNames)
            {
                result.Variables[name] = NumberSerializerFactory.Serialize(Memory[name]);
            }

            foreach (var name in Memory.ExpressionNames)
            {
                var value = Memory.GetExpression(name).ToString(CultureInfo.InvariantCulture);
                result.Expressions[name] = value;
            }

            await JsonSerializer.SerializeAsync(target, result, CreateOptions(), token);
        }
    }
}
