using CalculatorShell.Maths;
using NUnit.Framework;

namespace CalculatorShell.Tests.Maths
{
    [TestFixture]
    public class FractionTests
    {
        [TestCase(5, 5, 1, 1)]
        [TestCase(27, 3, 9, 1)]
        [TestCase(34, 8, 17, 4)]
        public void TestReduceOnCreate(long n1, long d1, long en, long ed)
        {
            Fraction f1 = new Fraction(n1, d1);
            Fraction expected = new Fraction(en, ed);
            Assert.AreEqual(expected, f1);
        }

        [TestCase(0.0625, 1, 16)]
        [TestCase(0.25, 1, 4)]
        [TestCase(0.125, 1, 8)]
        [TestCase(0.5, 1, 2)]
        [TestCase(0.00024414062, 12207031, 50000000000)]
        [TestCase(9.84438004e-5, 246109501, 2500000000000)]
        public void TestCreateFromDouble(double d, long en, long ed)
        {
            Fraction expected = new Fraction(en, ed);
            Assert.AreEqual(expected, new Fraction(d));
        }

        [TestCase(1, 2, 1, 3, 5, 6)]
        public void TestAdd(long n1, long d1, long n2, long d2, long en, long ed)
        {
            Fraction expected = new Fraction(en, ed);
            Fraction f1 = new Fraction(n1, d1);
            Fraction f2 = new Fraction(n2, d2);
            Assert.AreEqual(expected, f1 + f2);
        }

        [TestCase(3, 4, 1, 4, 2, 4)]
        public void TestSub(long n1, long d1, long n2, long d2, long en, long ed)
        {
            Fraction expected = new Fraction(en, ed);
            Fraction f1 = new Fraction(n1, d1);
            Fraction f2 = new Fraction(n2, d2);
            Assert.AreEqual(expected, f1 - f2);
        }

        [TestCase(3, 4, 1, 4, 3, 16)]
        public void TestMultiply(long n1, long d1, long n2, long d2, long en, long ed)
        {
            Fraction expected = new Fraction(en, ed);
            Fraction f1 = new Fraction(n1, d1);
            Fraction f2 = new Fraction(n2, d2);
            Assert.AreEqual(expected, f1 * f2);
        }

        [TestCase(3, 4, 1, 4, 3, 1)]
        public void TestDivide(long n1, long d1, long n2, long d2, long en, long ed)
        {
            Fraction expected = new Fraction(en, ed);
            Fraction f1 = new Fraction(n1, d1);
            Fraction f2 = new Fraction(n2, d2);
            Assert.AreEqual(expected, f1 / f2);
        }

    }
}
