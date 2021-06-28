using CalculatorShell.Ui;
using NUnit.Framework;
using System.Linq;

namespace CalculatorShell.Tests.Shell
{
    [TestFixture]
    public class LineReaderTests
    {
        private LineReader _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new LineReader(true, new FakeConsole(), null);
            _sut.AddHistory(new string[] { "ls -a", "dotnet run", "git init" });
        }

        [Test]
        public void TestNoInitialHistory()
        {
            Assert.AreEqual(3, _sut.GetHistory().Count);
        }

        [Test]
        public void TestUpdatesHistory()
        {
            _sut.AddHistory("mkdir");
            Assert.AreEqual(4, _sut.GetHistory().Count);
            Assert.AreEqual("mkdir", _sut.GetHistory().Last());
        }

        [Test]
        public void TestGetCorrectHistory()
        {
            Assert.AreEqual("ls -a", _sut.GetHistory()[0]);
            Assert.AreEqual("dotnet run", _sut.GetHistory()[1]);
            Assert.AreEqual("git init", _sut.GetHistory()[2]);
        }

    }
}
