using CalculatorShell.Expressions;
using NUnit.Framework;
using System.Globalization;

namespace CalculatorShell.Tests.Expressions
{
    [TestFixture]
    public class ExpressionFactoryTest
    {
        [TestCase("1!2")]
        [TestCase("1%2")]
        [TestCase("1'2")]
        [TestCase("**")]
        [TestCase("1Ö2")]
        [TestCase("1=2")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t")]
        [TestCase(" ")]
        [TestCase("\r\n")]
        [TestCase("\r")]
        [TestCase("\n")]
        [TestCase("sin(99ö)")]
        [TestCase("root(99,)")]
        [TestCase("root(,99)")]
        public void TestParseInvalidTokens(string expression)
        {
            Assert.Throws<ExpressionEngineException>(() =>
            {
                ExpressionFactory.Parse(expression, null, CultureInfo.InvariantCulture);
            });
        }

        [TestCase("1\t+1", "2")]
        [TestCase("1 +1", "2")]
        [TestCase("1\r+1", "2")]
        [TestCase("1\n+1", "2")]
        [TestCase("1\r\n+1", "2")]
        [TestCase("2^10", "1024")]
        [TestCase("1/2+1/4", "0.75")]
        [TestCase("(1/2)*2+(4*3)", "13")]
        [TestCase("1+-2", "-1")]
        [TestCase("-2", "-2")]
        [TestCase("33-11", "22")]
        [TestCase("10^2", "100")]
        [TestCase("true&true", "True")]
        [TestCase("true&false", "False")]
        [TestCase("true|true", "True")]
        [TestCase("true|false", "True")]
        [TestCase("false|false", "False")]
        [TestCase("!true", "False")]
        [TestCase("!false", "True")]
        [TestCase("!(!false)", "False")]
        [TestCase("ln(2)", "0.6931471805599453")]
        [TestCase("log(16; 2)", "4")]
        [TestCase("root(16; 2)", "4")]
        public void TestParseAndEvalute(string expression, string expected)
        {
            IExpression parsed = ExpressionFactory.Parse(expression, null, CultureInfo.InvariantCulture);

            INumber result = parsed.Evaluate();

            Assert.AreEqual(expected, result.ToString());
        }


    }
}
