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
        [TestCase("1_p", 1E-12)]
        [TestCase("1_n", 1E-9)]
        [TestCase("1_micro", 1E-6)]
        [TestCase("1_m", 1E-3)]
        [TestCase("1_d", 1E1)]
        [TestCase("1_h", 1E2)]
        [TestCase("1_k", 1E3)]
        [TestCase("1_M", 1E6)]
        [TestCase("1_G", 1E9)]
        [TestCase("1_T", 1E12)]
        [TestCase("1970_01_01", 0.0d)]
        [TestCase("01/01/1970", 0.0d)]
        [TestCase("01/01/1970 00:00", 0.0d)]
        [TestCase("01/01/1970 00:00:00", 0.0d)]
        [TestCase("06/16/2021 16:01:00", 1623859260d)]
        [TestCase("2021_06_16 16:01:00", 1623859260d)]
        [TestCase("1_000", 1000d)]
        [TestCase("0hff", 255)]
        [TestCase("0hf_f", 255)]
        [TestCase("0b1111", 15)]
        [TestCase("0b11_11", 15)]
        [TestCase("0o777", 511)]
        [TestCase("0o7_77", 511)]
        public void TestDoubleParsing(string input, double expected)
        {
            bool succes = NumberParser.TryParse(input, out NumberImplementation result, CultureInfo.InvariantCulture);
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
            bool succes = NumberParser.TryParse(input, out NumberImplementation result, CultureInfo.InvariantCulture);
            Assert.IsTrue(succes);
            Assert.AreEqual(NumberType.Boolean, result.NumberType);
            Assert.AreEqual(expected, result.Value);
        }
    }
}
