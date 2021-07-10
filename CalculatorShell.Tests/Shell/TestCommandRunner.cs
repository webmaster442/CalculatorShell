using CalculatorShell.Base;
using CalculatorShell.Infrastructure;
using Moq;
using NUnit.Framework;
using System;
using System.Globalization;

namespace CalculatorShell.Tests.Shell
{
    [TestFixture]
    public class TestCommandRunner
    {
        private Mock<IHostEx> _hostMock;
        private Mock<IFsHost> _fsHostMock;
        private Mock<IMemory> _memMock;
        private Mock<ICommandConsole> _consoleMock;

        private CommandRunner _sut;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _hostMock = new Mock<IHostEx>();
            _fsHostMock = new Mock<IFsHost>();
            _memMock = new Mock<IMemory>();
            _consoleMock = new Mock<ICommandConsole>();
            var loader = new CommandLoader(_memMock.Object, _hostMock.Object);

            _sut = new CommandRunner(loader.Commands, _hostMock.Object, _fsHostMock.Object, CultureInfo.InvariantCulture);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            _sut.Dispose();
            _sut = null;
        }

        [SetUp]
        public void Setup()
        {
            _fsHostMock.SetupGet(x => x.Home).Returns(Environment.CurrentDirectory);
        }

        private void Run(string cmd)
        {
            _sut.RunSingleCommand(cmd, _consoleMock.Object).Wait();
        }

        [Test]
        public void TestClear()
        {
            Run("cls");
            _consoleMock.Verify(x => x.Clear(), Times.Once);

            Run("clear");
            _consoleMock.Verify(x => x.Clear(), Times.Exactly(2));
        }

        [Test]
        public void TestExit()
        {
            Run("exit");
            _hostMock.Verify(x => x.Shutdown(), Times.Once);
        }

        [Test]
        public void TestCd()
        {
            Run("cd ~");
            _fsHostMock.VerifySet(x => x.CurrentDirectory = It.IsAny<string>());

            Run("cd ..");
            _fsHostMock.VerifySet(x => x.CurrentDirectory = It.IsAny<string>());

            Run("cd c:\\");
            _fsHostMock.VerifySet(x => x.CurrentDirectory = It.IsAny<string>());
        }
    }
}
