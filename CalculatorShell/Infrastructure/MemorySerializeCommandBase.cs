using CalculatorShell.Base.Domain;
using CalculatorShell.Expressions;
using System;
using System.Globalization;

namespace CalculatorShell.Infrastructure
{
    internal abstract class MemorySerializeCommandBase : CommandBase
    {

        protected void Load(MemoryData data)
        {
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

        protected MemoryData Save()
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

            return result;
        }
    }
}
