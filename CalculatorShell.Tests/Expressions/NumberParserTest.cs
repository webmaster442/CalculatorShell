using CalculatorShell.Expressions;
using NUnit.Framework;
using System.Globalization;

namespace CalculatorShell.Tests.Expressions
{
    [TestFixture]
    public class NumberParserTest
    {
        [TestCase("0", 0.0)]
        [TestCase("1.1", 1.1)]
        [TestCase("3.1415", 3.1415)]
        [TestCase("1p", 1E-12)]
        [TestCase("1n", 1E-9)]
        [TestCase("1micro", 1E-6)]
        [TestCase("1m", 1E-3)]
        [TestCase("1d", 1E1)]
        [TestCase("1h", 1E2)]
        [TestCase("1k", 1E3)]
        [TestCase("1M", 1E6)]
        [TestCase("1G", 1E9)]
        [TestCase("1T", 1E12)]
        [TestCase("1970-01-01", 0.0d)]
        [TestCase("2021-06-16 16:01:00", 1623859260d)]
        public void TestDoubleParsing(string input, double expected)
        {
            bool succes = NumberParser.TryParse(input, out var result, CultureInfo.InvariantCulture);
            Assert.IsTrue(succes);
            Assert.AreEqual(NumberType.Double, result.NumberType);
            Assert.AreEqual(expected, result.Value);
        }

        [TestCase("True", true)]
        [TestCase("False", false)]
        [TestCase("false", false)]
        [TestCase("true", true)]
        [TestCase("tRuE", true)]
        [TestCase("fAlSe", false)]
        public void TestBooleanParsing(string input, bool expected)
        {
            bool succes = NumberParser.TryParse(input, out var result, CultureInfo.InvariantCulture);
            Assert.IsTrue(succes);
            Assert.AreEqual(NumberType.Boolean, result.NumberType);
            Assert.AreEqual(expected, result.Value);
        }
    }
}
