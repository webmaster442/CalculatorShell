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

        [TestCase("((!a) & (!b))", 0)]
        [TestCase("((!a) & b)", 1)]
        [TestCase("(a & (!b))", 2)]
        [TestCase("(a & b)", 3)]
        [TestCase("(a | b)", 1, 2, 3)]
        [TestCase("((a & (!b)) | ((!a) & b))", 1, 2)]
        [TestCase("((a & b) | ((!a) & (!b)))", 0, 3)]
        [TestCase("True", 0, 1, 2, 3)]
        public void TestMintermParsingMsb(string expected, params int[] input)
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

        [TestCase("((!b) & (!a))", 0)]
        [TestCase("((!b) & a)", 1)]
        [TestCase("(b & (!a))", 2)]
        [TestCase("(b & a)", 3)]
        [TestCase("(b | a)", 1, 2, 3)]
        [TestCase("((b & (!a)) | ((!b) & a))", 1, 2)]
        [TestCase("((b & a) | ((!b) & (!a)))", 0, 3)]
        [TestCase("True", 0, 1, 2, 3)]
        public void TestMintermParsingLsb(string expected, params int[] input)
        {
            IExpression parsed = ExpressionFactory.ParseLogic(input, null, new ParseLogicOptions
            {
                LsbDirection = Lsb.AisLsb,
                Variables = _variablesMock.Object,
                Culture = CultureInfo.InvariantCulture,
                GenerateHazardFree = false,
                TermKind = TermKind.Minterm,
            });
            Assert.AreEqual(expected, parsed.ToString());
        }
    }
}
