using CalculatorShell.Expressions;
using NUnit.Framework;
using System.Globalization;

namespace CalculatorShell.Tests.Expressions
{
    [TestFixture]
    public class UnitConversionTests
    {
        private UnitConverter _sut;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _sut = new UnitConverter(CultureInfo.InvariantCulture);
        }

        
        [TestCase("1", "kiB", "byte", "1024")]
        [TestCase("1", "MiB", "kiB", "1024")]
        [TestCase("1", "GiB", "MiB", "1024")]
        [TestCase("1", "TiB", "GiB", "1024")]
        [TestCase("1", "PiB", "TiB", "1024")]
        public void ConvertTest(string inputValue, string inputUnit, string targetUnit, string expected)
        {
            var result = _sut.Convert(inputValue, inputUnit, targetUnit);
            Assert.AreEqual(expected, result);
        }
    }
}
