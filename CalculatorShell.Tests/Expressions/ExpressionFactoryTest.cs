using CalculatorShell.Expressions;
using CalculatorShell.Maths;
using Moq;
using NUnit.Framework;
using System.Globalization;

namespace CalculatorShell.Tests.Expressions
{
    [TestFixture]
    public class ExpressionFactoryTest
    {
        private Mock<IVariables> _variablesMock;

        [SetUp]
        public void Setup()
        {
            _variablesMock = new Mock<IVariables>(MockBehavior.Strict);
            _variablesMock.SetupGet(x => x["a"]).Returns(new NumberImplementation(44));
            _variablesMock.SetupGet(x => x["b"]).Returns(new NumberImplementation(new Fraction(3, 4)));
            _variablesMock.SetupGet(x => x["b", "Denominator"]).Returns(new NumberImplementation(4));
            _variablesMock.SetupGet(x => x["b", "denominator"]).Returns(new NumberImplementation(4));
        }

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

        [TestCase("0hff+1", "256")]
        [TestCase("0b1111*2", "30")]
        [TestCase("0o777-11", "500")]
        [TestCase("1\t+1", "2")]
        [TestCase("1 +1", "2")]
        [TestCase("1\r+1", "2")]
        [TestCase("1\n+1", "2")]
        [TestCase("1\r\n+1", "2")]
        [TestCase("2^10", "1024")]
        [TestCase("(1/2)*2+(4*3)", "13")]
        [TestCase("1+-2", "-1")]
        [TestCase("-2", "-2")]
        [TestCase("33-11", "22")]
        [TestCase("10^2", "100")]
        [TestCase("(1/27)", "1/27")]
        [TestCase("1/27", "1/27")]
        [TestCase("(1/3)+(1/3)+(1/3)", "1")]
        [TestCase("(2/3)-(1/3)", "1/3")]
        [TestCase("(1/3)*(2*4)", "8/3")]
        [TestCase("(1/3)/(2*4)", "1/24")]
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
        [TestCase("sin(90)", "1")]
        [TestCase("cos(90)", "0")]
        [TestCase("tan(90)", "Infinity")]
        [TestCase("abs(-1)", "1")]
        [TestCase("abs(1)", "1")]
        [TestCase("floor(3.5)", "3")]
        [TestCase("ceil(3.5)", "4")]
        [TestCase("sign(-5)", "-1")]
        [TestCase("sign(5)", "1")]
        [TestCase("a+1", "45")]
        [TestCase("sign(a)", "1")]
        [TestCase("b[Denominator]", "4")]
        [TestCase("b[denominator]", "4")]
        [TestCase("cplx(10; 20) + cplx(10; 20)", "(20 + 40i)")]
        [TestCase("cplx(10; 20) + 10", "(20 + 20i)")]
        [TestCase("cplx(10; 20) - cplx(5; 3)", "(5 + 17i)")]
        [TestCase("cplx(10; 20) - 10", "(0 + 20i)")]
        [TestCase("cplx(10; 20) * 10", "(100 + 200i)")]
        [TestCase("cplx(10; 20) * cplx(10; 20)", "(-300 + 400i)")]
        [TestCase("cplx(10; 20) / cplx(10; 20)", "(1 + 0i)")]
        [TestCase("cplx(10; 20) / 10", "(1 + 2i)")]
        public void TestsParseAndEvalute(string expression, string expected)
        {
            ExpressionFactory.CurrentAngleMode = CalculatorShell.Maths.AngleMode.Deg;
            IExpression parsed = ExpressionFactory.Parse(expression, _variablesMock.Object, CultureInfo.InvariantCulture);

            INumber result = parsed.Evaluate();

            Assert.AreEqual(expected, result.ToString());
        }
    }
}
