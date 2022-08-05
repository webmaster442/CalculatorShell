using CalculatorShell.Expressions;
using NUnit.Framework;
using System.Globalization;

namespace CalculatorShell.Tests.Expressions
{
    [TestFixture]
    public class ExtensionsTests
    {
        private TestVars _testVars;

        [OneTimeSetUp]
        public void Setup()
        {
            _testVars = new TestVars();
        }

        [TestCase("a&b", true)]
        [TestCase("a|b", true)]
        [TestCase("!a&b", true)]
        [TestCase("!(a&b)", true)]
        [TestCase("(a&b)|c", true)]
        [TestCase("a&true", true)]
        [TestCase("a&false", true)]
        [TestCase("1+2", false)]
        public void TestIsLogicExpression(string expression, bool expected)
        {
            IExpression parsed = ExpressionFactory.Parse(expression, _testVars, CultureInfo.InvariantCulture);
            bool result = parsed.IsLogicExpression();
            Assert.AreEqual(expected, result);
        }

        [TestCase("a&b", 3)]
        [TestCase("a|b", 1, 2, 3)]
        [TestCase("(!a)&(!b)", 0)]
        public void TestMinterms(string expression, params int[] expected)
        {
            IExpression parsed = ExpressionFactory.Parse(expression, _testVars, CultureInfo.InvariantCulture);
            int[] result = parsed.GetMinterms().ToArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestCase("a|b|b", "(a | b)")]
        [TestCase("a|!a", "True")]
        [TestCase("a&!a", "False")]
        [TestCase("true|false", "True")]
        [TestCase("true&false", "False")]
        public void TestExtendedSimplify(string expression, string expected)
        {
            IExpression parsed = ExpressionFactory.Parse(expression, _testVars, CultureInfo.InvariantCulture);
            IExpression simplified = parsed.ExtendedSimplify(CultureInfo.InvariantCulture);

            Assert.AreEqual(expected, simplified.ToString());

        }
    }
}
