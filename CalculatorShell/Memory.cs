using CalculatorShell.Expressions;
using CalculatorShell.Infrastructure;
using CalculatorShell.Properties;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CalculatorShell
{
    internal class Memory : IMemory
    {
        private readonly Dictionary<string, dynamic> _variables;
        private readonly Dictionary<string, dynamic> _constants;

        public Memory()
        {
            _variables = new Dictionary<string, dynamic>();
            _constants = FillConstants();
        }

        private Dictionary<string, dynamic> FillConstants()
        {
            var fields = typeof(Constants).GetFields(BindingFlags.Static | BindingFlags.Public);
            return fields.ToDictionary(x => x.Name, x => x.GetValue(null));
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

                if (_variables.ContainsKey(variable))
                    _variables[variable] = value;
                else
                    _variables.Add(variable, value);
            }
        }

        public int Count => _variables.Count + _constants.Count;

        public IEnumerable<string> VariableNames => _variables.Keys.Union(_constants.Keys);

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
    }
}
