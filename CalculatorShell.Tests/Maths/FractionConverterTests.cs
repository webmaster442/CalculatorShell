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
            (Fraction fraction, double error) = FractionConverter.ConvertToFraction(input, digits);
            Assert.AreEqual(numerator, fraction.Numerator);
            Assert.AreEqual(denominator, fraction.Denominator);
            Assert.AreEqual(error, expectedError, 1e-9);

        }

        [TestCase(1L, 4L, 0.25d, 0.0d)]
        [TestCase(1L, 2L, 0.5d, 0.0d)]
        public void ConvertToDoubleTest(long numerator, long denominator, double expected, double expectedError)
        {
            (double number, double error) = FractionConverter.ConvertToDouble(new Fraction(numerator, denominator));
            Assert.AreEqual(expected, number, 1E-9);
            Assert.AreEqual(expectedError, error, 1E-9);

        }
    }
}
