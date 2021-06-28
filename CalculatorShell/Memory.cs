using CalculatorShell.Base;
using CalculatorShell.Expressions;
using CalculatorShell.Properties;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CalculatorShell
{
    internal class Memory : IMemory
    {
        private Dictionary<string, INumber> _variables;
        private readonly Dictionary<string, dynamic> _constants;

        public Memory()
        {
            _variables = new Dictionary<string, INumber>();
            _constants = FillConstants();
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

        public INumber this[string variable]
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

        public string this[string variable, string property]
        {
            get
            {
                INumber var = this[variable];
                return var.GetPropertyValue(property);
            }
        }

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
    }
}
