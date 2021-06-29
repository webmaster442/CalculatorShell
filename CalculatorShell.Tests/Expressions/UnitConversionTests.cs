﻿using CalculatorShell.Expressions;
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
        [TestCase("1", "meter", "foot", "3.280839895")]
        [TestCase("1", "foot", "meter", "0.3048")]
        [TestCase("1", "meter", "centi_meter", "100")]
        [TestCase("1", "meter", "mili_meter", "1000")]
        [TestCase("1", "kilo_meter", "meter", "1000")]
        [TestCase("1", "deci_meter", "centi_meter", "10")]
        [TestCase("1", "kilo_foot", "meter", "304.8")]
        [TestCase("1000", "yard", "inch", "36000")]
        [TestCase("1", "kilo_yard", "inch", "36000")]
        [TestCase("1", "kilo_yard", "centi_inch", "360000")]
        [TestCase("36000", "inch", "yard", "1000")]
        public void ConvertTest(string inputValue, string inputUnit, string targetUnit, string expected)
        {
            var result = _sut.Convert(inputValue, inputUnit, targetUnit);
            if (result.StartsWith(expected))
            {
                Assert.Pass();
            }
            Assert.Fail($"Expected: {expected}, was: {result}");
        }
    }
}
