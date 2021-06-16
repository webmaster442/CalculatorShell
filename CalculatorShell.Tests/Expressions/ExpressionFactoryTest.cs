﻿using CalculatorShell.Expressions;
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
        /*[TestCase("3*pi", 9.4247779607693797153879301498385d)]
        [TestCase("3*PI", 9.4247779607693797153879301498385d)]
        [TestCase("sin(90)", 1d)]
        [TestCase("cos(90)", 0d)]
        [TestCase("tan(90)", double.PositiveInfinity)]
        [TestCase("tan(180)", 0)]
        [TestCase("tan(270)", double.PositiveInfinity)]
        [TestCase("tan(360)", 0)]
        [TestCase("ctg(0)", double.PositiveInfinity)]
        [TestCase("ctg(90)", 0)]
        [TestCase("ctg(180)", double.NegativeInfinity)]
        [TestCase("ctg(270)", 0)]
        [TestCase("ctg(360)", double.NegativeInfinity)]
        [TestCase("root(2, 2)", 1.4142135623730950488)]
        [TestCase("ln(e)", 1)]
        [TestCase("ln(100)", 4.6051701859880913680359829093687)]
        [TestCase("log(1024,2)", 10)]
        [TestCase("factorial(5)", 120)]
        [TestCase("gamma(5)", 24)]
        [TestCase("0&0", 0)]
        [TestCase("0&1", 0)]
        [TestCase("1&0", 0)]
        [TestCase("1&1", 1)]
        [TestCase("0|0", 0)]
        [TestCase("0|1", 1)]
        [TestCase("1|0", 1)]
        [TestCase("1|1", 1)]
        [TestCase("!0", 1)]
        [TestCase("!1", 0)]
        [TestCase("!(!1)", 1)]
        [TestCase("!0&!0", 1)]
        [TestCase("false", 0)]
        [TestCase("true", 1)]
        [TestCase("sin(y)", 1)]
        [TestCase("y^2", 8100)]
        [TestCase("y-2", 88)]
        [TestCase("y+2", 92)]
        [TestCase("y*2", 180)]
        [TestCase("y/2", 45)]*/
        public void TestParseAndEvalute(string expression, string expected)
        {
            IExpression parsed = ExpressionFactory.Parse(expression, null, CultureInfo.InvariantCulture);

            INumber result = parsed.Evaluate();

            Assert.AreEqual(expected, result.ToString());
        }


    }
}
