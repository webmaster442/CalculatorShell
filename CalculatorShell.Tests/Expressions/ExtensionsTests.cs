using CalculatorShell.Expressions;
using Moq;
using NUnit.Framework;
using System.Globalization;

namespace CalculatorShell.Tests.Expressions
{
    [TestFixture]
    public class ExtensionsTests
    {
        private Mock<IVariables> _variablesMock;

        [OneTimeSetUp]
        public void Setup()
        {
            _variablesMock = new Mock<IVariables>();
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
            var parsed = ExpressionFactory.Parse(expression, _variablesMock.Object, CultureInfo.InvariantCulture);
            var result = parsed.IsLogicExpression();
            Assert.AreEqual(expected, result);
        }
    }
}
