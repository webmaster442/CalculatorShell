using CalculatorShell.Base;
using CalculatorShell.Expressions;
using CalculatorShell.Properties;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace CalculatorShell
{
    internal class Memory : IMemory
    {
        private Dictionary<string, dynamic> _variables;
        private readonly Dictionary<string, dynamic> _constants;
        private readonly JsonSerializerOptions _serializeOptions;

        public Memory()
        {
            _variables = new Dictionary<string, dynamic>();
            _constants = FillConstants();
            _serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals,
                PropertyNameCaseInsensitive = false,
            };
        }

        private static Dictionary<string, dynamic> FillConstants()
        {
            var fields = typeof(Constants).GetFields(BindingFlags.Static | BindingFlags.Public);
            if (fields != null)
            {
                return fields.ToDictionary(x => x.Name, x => x.GetValue(null))!;
            }
            return new Dictionary<string, dynamic>();
        }

        public dynamic this[string variable]
        {
            get
            {
                if (IsConstant(variable))
                    return _constants[variable];
                else if (IsVariable(variable))
                    return _variables[variable];

                throw new ExpressionEngineException(Resources.UndefinedVariable, variable);
            }
            set
            {
                if (IsConstant(variable))
                    throw new ExpressionEngineException(Resources.ConstantCantBeRedefined, variable);

                _variables[variable] = value;
            }
        }

        public int Count => _variables.Count + _constants.Count;

        public IEnumerable<string> VariableAndConstantNames => _variables.Keys.Union(_constants.Keys);

        public IEnumerable<string> VariableNames => _variables.Keys;

        public void Clear()
        {
            _variables.Clear();
        }

        public bool IsConstant(string variableName)
        {
            return _constants.ContainsKey(variableName);
        }

        public bool IsVariable(string variableName)
        {
            return _variables.ContainsKey(variableName);
        }

        public void Delete(string name)
        {
            _variables.Remove(name);
        }

        public void WriteToFile(string fileName)
        {
            var result = JsonSerializer.Serialize(_variables, _serializeOptions);
            System.IO.File.WriteAllText(fileName, result);
        }

        public void ReadFromFile(string fileName)
        {
            var json = System.IO.File.ReadAllText(fileName);
            var deserialized = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(json, _serializeOptions);
            if (deserialized != null)
            {
                _variables = deserialized;
            }
        }
    }
}
