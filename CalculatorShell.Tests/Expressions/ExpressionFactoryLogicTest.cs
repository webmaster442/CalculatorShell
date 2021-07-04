using CalculatorShell.Expressions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorShell.Tests.Expressions
{
    [TestFixture]
    public class ExpressionFactoryLogicTest
    {
        private Mock<IVariables> _variablesMock;

        [SetUp]
        public void Setup()
        {
            _variablesMock = new Mock<IVariables>();
        }

        /*[TestCase("(a & b)", 3)]
        [TestCase("(a | b)", 1, 2, 3)]*/
        public void TestMintermParsing(string expected, params int[] input)
        {
            IExpression parsed = ExpressionFactory.ParseLogic(input, null, new ParseLogicOptions
            {
                LsbDirection = Lsb.AisMsb,
                Variables = _variablesMock.Object,
                Culture = CultureInfo.InvariantCulture,
                GenerateHazardFree = false,
                TermKind = TermKind.Minterm,
            });
            Assert.AreEqual(expected, parsed.ToString());
        }
    }
}
