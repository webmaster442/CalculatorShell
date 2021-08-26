using CalculatorShell.Expressions.Internals;
using NUnit.Framework;

namespace CalculatorShell.Tests.Expressions
{
    [TestFixture]
    public class VariableNameCheckerTests
    {
        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("\n", false)]
        [TestCase("\r\n", false)]
        [TestCase("\t", false)]
        [TestCase(null, false)]
        [TestCase("12df", false)]
        [TestCase("üü", false)]
        [TestCase("$üü", false)]
        [TestCase("*ó", false)]
        [TestCase("foo bar", false)]
        [TestCase("foo\tbar", false)]
        [TestCase("foo\r\nbar", false)]
        [TestCase("foo", true)]
        [TestCase("foo_bar", true)]
        [TestCase("foo1234", true)]
        [TestCase("p123p", true)]
        public void IsValidVariableNameTest(string input, bool expected)
        {
            bool actual = VariableNameChecker.IsValidVariableName(input);
            Assert.AreEqual(expected, actual);
        }

    }
}
