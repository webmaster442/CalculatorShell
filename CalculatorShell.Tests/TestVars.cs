using CalculatorShell.Expressions;

namespace CalculatorShell.Tests
{
    internal class TestVars : IVariables
    {
        private readonly Dictionary<string, INumber> _vars;

        public TestVars()
        {
            _vars = new Dictionary<string, INumber>();
        }

        public INumber this[string variable]
        {
            get => _vars[variable];
            set => _vars[variable] = value;
        }

        public INumber this[string variable, string property] => throw new NotImplementedException();

        public IEnumerable<string> VariableNames => _vars.Keys;

        public void Clear()
        {
            _vars.Clear();
        }

        public bool IsConstant(string variableName)
        {
            return false;
        }

        public bool IsVariable(string variableName)
        {
            return true;
        }
    }
}
