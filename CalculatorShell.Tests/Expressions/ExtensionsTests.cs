using CalculatorShell.Expressions;
using Moq;
using NUnit.Framework;
using System.Globalization;
using System.Linq;

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
            var parsed = ExpressionFactory.Parse(expression, _testVars, CultureInfo.InvariantCulture);
            var result = parsed.IsLogicExpression();
            Assert.AreEqual(expected, result);
        }

        [TestCase("a&b", 3)]
        [TestCase("a|b", 1, 2, 3)]
        [TestCase("(!a)&(!b)", 0)]
        public void TestMinterms(string expression, params int[] expected)
        {
            var parsed = ExpressionFactory.Parse(expression, _testVars, CultureInfo.InvariantCulture);
            var result = parsed.GetMinterms().ToArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestCase("a|b|b", "(a | b)")]
        [TestCase("a|!a", "True")]
        [TestCase("a&!a", "False")]
        [TestCase("true|false", "True")]
        [TestCase("true&false", "False")]
        public void TestExtendedSimplify(string expression, string expected)
        {
            var parsed = ExpressionFactory.Parse(expression, _testVars, CultureInfo.InvariantCulture);
            var simplified = parsed.ExtendedSimplify(CultureInfo.InvariantCulture);

            Assert.AreEqual(expected, simplified.ToString());

        }
    }
}
