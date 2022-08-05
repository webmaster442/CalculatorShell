using CalculatorShell.Maths;
using NUnit.Framework;

namespace CalculatorShell.Tests.Maths
{
    [TestFixture]
    public class ExtensionTests
    {
        [TestCase(1970, 1, 1, 0)]
        [TestCase(2021, 6, 16, 1623801600)]
        public void TestToUnixTime(int year, int month, int day, double expected)
        {
            DateTime d = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
            Assert.AreEqual(expected, d.ToUnixTime());
        }

        [TestCase(0, 1970, 1, 1)]
        [TestCase(1623801600, 2021, 6, 16)]
        public void Test(double d, int year, int month, int day)
        {
            DateTime expected = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
            Assert.AreEqual(expected, d.ToDateTime());
        }

        [TestCase(3, true)]
        [TestCase(3.14, false)]
        [TestCase(3.0000001, false)]
        public void TestIsInteger(double input, bool expected)
        {
            Assert.AreEqual(expected, input.IsInteger());
        }

        [TestCase(3.14d, 3L)]
        [TestCase(3.99d, 4L)]
        public void TestToInteger(double input, long expected)
        {
            Assert.AreEqual(expected, input.ToInteger());
        }
    }
}
