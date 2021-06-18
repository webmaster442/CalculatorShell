using CalculatorShell.Maths;
using NUnit.Framework;

namespace CalculatorShell.Tests.Maths
{
    [TestFixture]
    [SingleThreaded]
    public class TrigonometryTests
    {
        [TestCase(0, 0)]
        [TestCase(15, 0.258819045)]
        [TestCase(30, 0.5)]
        [TestCase(45, 0.707106781)]
        [TestCase(60, 0.866025404)]
        [TestCase(75, 0.965925826)]
        [TestCase(90, 1)]
        [TestCase(105, 0.965925826)]
        [TestCase(120, 0.866025404)]
        [TestCase(135, 0.707106781)]
        [TestCase(150, 0.5)]
        [TestCase(165, 0.258819045)]
        [TestCase(180, 0)]
        [TestCase(195, -0.258819045)]
        [TestCase(210, -0.5)]
        [TestCase(225, -0.707106781)]
        [TestCase(240, -0.866025404)]
        [TestCase(255, -0.965925826)]
        [TestCase(270, -1)]
        [TestCase(285, -0.965925826)]
        [TestCase(300, -0.866025404)]
        [TestCase(315, -0.707106781)]
        [TestCase(330, -0.5)]
        [TestCase(345, -0.258819045)]
        [TestCase(360, 0)]

        public void TestSinAngle(double input, double expected)
        {
            Trigonometry.AngleMode = AngleMode.Deg;
            Assert.AreEqual(expected, Trigonometry.Sin(input), 1E-6);
        }

        [TestCase(0, 0)]
        [TestCase(0.5, 30)]
        [TestCase(1, 90)]
        [TestCase(-0.5, -30)]
        [TestCase(-1, -90)]
        public void TestArcSinAngle(double input, double expected)
        {
            Trigonometry.AngleMode = AngleMode.Deg;
            Assert.AreEqual(expected, Trigonometry.ArcSin(input), 1E-6);
        }

        [TestCase(0, 0)]
        [TestCase(100, 1)]
        [TestCase(200, 0)]
        [TestCase(300, -1)]
        [TestCase(400, 0)]
        public void TestSinGrad(double input, double expected)
        {
            Trigonometry.AngleMode = AngleMode.Grad;
            Assert.AreEqual(expected, Trigonometry.Sin(input), 1E-6);
        }

        [TestCase(0, 1)]
        [TestCase(15, 0.965925826)]
        [TestCase(30, 0.866025404)]
        [TestCase(45, 0.707106781)]
        [TestCase(60, 0.5)]
        [TestCase(75, 0.258819045)]
        [TestCase(90, 0)]
        [TestCase(105, -0.258819045)]
        [TestCase(120, -0.5)]
        [TestCase(135, -0.707106781)]
        [TestCase(150, -0.866025404)]
        [TestCase(165, -0.965925826)]
        [TestCase(180, -1)]
        [TestCase(195, -0.965925826)]
        [TestCase(210, -0.866025404)]
        [TestCase(225, -0.707106781)]
        [TestCase(240, -0.5)]
        [TestCase(255, -0.258819045)]
        [TestCase(270, 0)]
        [TestCase(285, 0.258819045)]
        [TestCase(300, 0.5)]
        [TestCase(315, 0.707106781)]
        [TestCase(330, 0.866025404)]
        [TestCase(345, 0.965925826)]
        [TestCase(360, 1)]

        public void TestCosAngle(double input, double expected)
        {
            Trigonometry.AngleMode = AngleMode.Deg;
            Assert.AreEqual(expected, Trigonometry.Cos(input), 1E-6);
        }

        [TestCase(1, 0)]
        [TestCase(0.5, 60)]
        [TestCase(-0.5, 120)]
        [TestCase(-1, 180)]
        public void TestArcCosAngle(double input, double expected)
        {
            Trigonometry.AngleMode = AngleMode.Deg;
            Assert.AreEqual(expected, Trigonometry.ArcCos(input), 1E-6);
        }

        [TestCase(0, 0)]
        [TestCase(15, 0.267949192)]
        [TestCase(30, 0.577350269)]
        [TestCase(45, 1)]
        [TestCase(60, 1.732050808)]
        [TestCase(75, 3.732050808)]
        [TestCase(90, double.PositiveInfinity)]
        [TestCase(105, -3.732050808)]
        [TestCase(120, -1.732050808)]
        [TestCase(135, -1)]
        [TestCase(150, -0.577350269)]
        [TestCase(165, -0.267949192)]
        [TestCase(180, 0)]
        [TestCase(195, 0.267949192)]
        [TestCase(210, 0.577350269)]
        [TestCase(225, 1)]
        [TestCase(240, 1.732050808)]
        [TestCase(255, 3.732050808)]
        [TestCase(270, double.PositiveInfinity)]
        [TestCase(285, -3.732050808)]
        [TestCase(300, -1.732050808)]
        [TestCase(315, -1)]
        [TestCase(330, -0.577350269)]
        [TestCase(345, -0.267949192)]
        [TestCase(360, 0)]

        public void TestTanAngle(double input, double expected)
        {
            Trigonometry.AngleMode = AngleMode.Deg;
            Assert.AreEqual(expected, Trigonometry.Tan(input), 1E-6);
        }
    }
}
