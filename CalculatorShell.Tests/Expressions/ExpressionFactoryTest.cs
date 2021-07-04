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
        private NumberImplementation _xValue;

        [SetUp]
        public void Setup()
        {
            _xValue = new NumberImplementation(0.0);
            _variablesMock = new Mock<IVariables>(MockBehavior.Strict);
            _variablesMock.SetupGet(x => x["a"]).Returns(new NumberImplementation(44));
            _variablesMock.SetupGet(x => x["one"]).Returns(new NumberImplementation(1));
            _variablesMock.SetupGet(x => x["minusone"]).Returns(new NumberImplementation(-1));
            _variablesMock.SetupGet(x => x["zero"]).Returns(new NumberImplementation(0));
            _variablesMock.SetupGet(x => x["b"]).Returns(new NumberImplementation(new Fraction(3, 4)));
            _variablesMock.SetupGet(x => x["b", "Denominator"]).Returns(new NumberImplementation(4));
            _variablesMock.SetupGet(x => x["b", "denominator"]).Returns(new NumberImplementation(4));
            _variablesMock.SetupGet(x => x["x"]).Returns(_xValue);
            _variablesMock.Setup(x => x.IsConstant(It.IsAny<string>())).Returns(false);
            _variablesMock.SetupSet(x => x["x"] = It.IsAny<INumber>()).Callback((string name, INumber value) =>
            {
                _xValue = value as NumberImplementation;
            });
        }

        [TestCase("1!2")]
        [TestCase("1x")]
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
        [TestCase("3%2", "1")]
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
        [TestCase("arcsin(1)", "90")]
        [TestCase("arccos(0)", "90")]
        [TestCase("arctan(1)", "45")]
        [TestCase("arcctg(0)", "0")]
        [TestCase("deg2rad(360)", "6.283185307179586")]
        [TestCase("rad2deg(0)", "0")]
        [TestCase("deg2grad(360)", "400")]
        [TestCase("grad2deg(400)", "360")]
        [TestCase("grad2rad(0)", "0")]
        [TestCase("rad2grad(0)", "0")]
        public void TestsParseAndEvalute(string expression, string expected)
        {
            ExpressionFactory.CurrentAngleMode = CalculatorShell.Maths.AngleMode.Deg;
            IExpression parsed = ExpressionFactory.Parse(expression, _variablesMock.Object, CultureInfo.InvariantCulture);

            INumber result = parsed.Evaluate();

            Assert.AreEqual(expected, result.ToString());
        }

        //Constants
        [TestCase("3", "0")]
        [TestCase("99", "0")]
        [TestCase("360", "0")]
        //1st order
        [TestCase("x", "1")]
        [TestCase("3*x", "3")]
        [TestCase("3*x+55", "3")]
        //2nd order
        [TestCase("x^2", "(2 * x)")]
        [TestCase("x^2+3*x", "((2 * x) + 3)")]
        [TestCase("x^2+3*x+22", "((2 * x) + 3)")]
        [TestCase("x^3", "(3 * (x ^ 2))")]
        //inverse
        [TestCase("1/x", "(-1 / (x ^ 2))")]
        //exponent
        [TestCase("2^x", "(0.6931471805599453 * (2 ^ x))")]
        //trigonometry
        [TestCase("sin(x)", "cos(x)")]
        [TestCase("cos(x)", "(-sin(x))")]
        [TestCase("tan(x)", "(cos(x) ^ -2)")]
        [TestCase("ctg(x)", "(-(sin(x) ^ -2))")]
        //Root
        [TestCase("root(x;2)", "(0.5 * (x ^ -0.5))")]
        //Logarithms
        [TestCase("ln(x)", "(1 / x)")]
        [TestCase("log(x;4)", "(0.7213475204444817 * (1 / x))")]
        public void TestDerive(string expression, string expected)
        {
            IExpression parsed = ExpressionFactory.Parse(expression, _variablesMock.Object, CultureInfo.InvariantCulture);

            var result = parsed.Differentiate("x").Simplify();

            Assert.AreEqual(expected, result.ToString());
        }

        [TestCase("1+1", "2")]
        [TestCase("0+one", "one")]
        [TestCase("one+0", "one")]
        [TestCase("one+-one", "(one - one)")]
        [TestCase("-one+-one", "((-one) - one)")]
        [TestCase("-one+one", "(one - one)")]
        [TestCase("1-1", "0")]
        [TestCase("0-one", "(-one)")]
        [TestCase("one-0", "one")]
        [TestCase("one--one", "(one + one)")]
        [TestCase("4/2", "2")]
        [TestCase("3.1/0.1", "31")]
        [TestCase("0/one", "0")]
        [TestCase("one/1", "one")]
        [TestCase("one/-1", "(-one)")]
        [TestCase("-one/-one", "(one / one)")]
        [TestCase("3*2", "6")]
        [TestCase("0*one", "0")]
        [TestCase("one*0", "0")]
        [TestCase("1*one", "one")]
        [TestCase("one*1", "one")]
        [TestCase("-1*one", "(-one)")]
        [TestCase("one*-1", "(-one)")]
        [TestCase("-one*-one", "(one * one)")]
        public void TestSimplify(string expression, string expected)
        {
            IExpression parsed = ExpressionFactory.Parse(expression, _variablesMock.Object, CultureInfo.InvariantCulture);

            var result = parsed.Simplify();

            Assert.AreEqual(expected, result.ToString());
        }
    }
}
