using CalculatorShell.Maths;
using NUnit.Framework;

namespace CalculatorShell.Tests.Maths
{
    [TestFixture]
    public class FractionConverterTests
    {

        [TestCase(0.5, 1, 1L, 2L, 0.0)]
        [TestCase(0.25, 2, 1L, 4L, 0.0)]
        public void ConvertToFractionTest(double input, int digits, long numerator, long denominator, double expectedError)
        {
            var result = FractionConverter.ConvertToFraction(input, digits);
            Assert.AreEqual(numerator, result.fraction.Numerator);
            Assert.AreEqual(denominator, result.fraction.Denominator);
            Assert.AreEqual(result.error, expectedError, 1e-9);

        }

        [TestCase(1L, 4L, 0.25d, 0.0d)]
        [TestCase(1L, 2L, 0.5d, 0.0d)]
        public void ConvertToDoubleTest(long numerator, long denominator, double expected, double expectedError)
        {
            var result = FractionConverter.ConvertToDouble(new Fraction(numerator, denominator));
            Assert.AreEqual(expected, result.number, 1E-9);
            Assert.AreEqual(expectedError, result.error, 1E-9);

        }
    }
}
