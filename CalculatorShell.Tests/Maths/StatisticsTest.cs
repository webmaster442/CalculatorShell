using CalculatorShell.Maths;
using NUnit.Framework;

namespace CalculatorShell.Tests.Maths
{
    [TestFixture]
    public class StatisticsTest
    {
        private StatisticResult _sut;

        [OneTimeSetUp]
        public void Setup()
        {
            _sut = Statistics.Calculate(-5, -4, -3, -2, -1, 0, 1, 2, 3, 3, 4, 4, 5);
        }

        [Test]
        public void TestMinimum()
        {
            Assert.AreEqual(-5, _sut.Minimum);
        }

        [Test]
        public void TestMaximum()
        {
            Assert.AreEqual(5, _sut.Maximum);
        }

        [Test]
        public void TestAverage()
        {
            Assert.AreEqual(0.53846153846153844, _sut.Average);
        }

        [Test]
        public void TestSum()
        {
            Assert.AreEqual(7, _sut.Sum);
        }

        [Test]
        public void TestCount()
        {
            Assert.AreEqual(13, _sut.Count);
        }

        [Test]
        public void TestRange()
        {
            Assert.AreEqual(10, _sut.Range);
        }

        [Test]
        public void TestMedian()
        {
            Assert.AreEqual(1, _sut.Median);
        }
    }
}
