using CalculatorShell.ReadLine;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

using static CalculatorShell.Tests.Shell.ConsoleKeyInfoExtensions;

namespace CalculatorShell.Tests.Shell
{
    [TestFixture]
    public class KeyHandlerTests
    {

        private KeyHandler _sut;
        private List<string> _history;
        private Mock<IAutoCompleteHandler> _autoCompleteHandlerMock;
        private FakeConsole _console;
        private string[] _completions;

        [SetUp]
        public void Setup()
        {
            _console = new FakeConsole();
            _autoCompleteHandlerMock = new Mock<IAutoCompleteHandler>(MockBehavior.Strict);
            _history = new List<string>(new string[] { "dotnet run", "git init", "clear" });
            _sut = new KeyHandler(_console, _history, null);

            _autoCompleteHandlerMock.SetupGet(x => x.Separators).Returns(new char[] { ' ', '.', '/', '\\', ':' });
            _autoCompleteHandlerMock.Setup(x => x.GetSuggestions(It.IsAny<string>(), It.IsAny<int>())).Returns(new string[] { "World", "Angel", "Love" });
            _completions = new string[] { "World", "Angel", "Love" };

            "Hello".Select(c => c.ToConsoleKeyInfo())
                .ToList()
                .ForEach(_sut.Handle);
        }

        [Test]
        public void TestWriteChar()
        {
            Assert.AreEqual("Hello", _sut.Text);

            " World".Select(c => c.ToConsoleKeyInfo())
                    .ToList()
                    .ForEach(_sut.Handle);

            Assert.AreEqual("Hello World", _sut.Text);
        }

        [Test]
        public void TestBackspace()
        {
            _sut.Handle(Backspace);
            Assert.AreEqual("Hell", _sut.Text);
        }

        [Test]
        public void TestDelete()
        {
            new List<ConsoleKeyInfo>() { LeftArrow, Delete }
                .ForEach(_sut.Handle);

            Assert.AreEqual("Hell", _sut.Text);
        }

        [Test]
        public void TestDelete_EndOfLine()
        {
            _sut.Handle(Delete);
            Assert.AreEqual("Hello", _sut.Text);
        }

        [Test]
        public void TestControlH()
        {
            _sut.Handle(CtrlH);
            Assert.AreEqual("Hell", _sut.Text);
        }

        [Test]
        public void TestControlT()
        {
            var initialCursorCol = _console.CursorLeft;
            _sut.Handle(CtrlT);

            Assert.AreEqual("Helol", _sut.Text);
            Assert.AreEqual(initialCursorCol, _console.CursorLeft);
        }

        [Test]
        public void TestControlT_LeftOnce_CursorMovesToEnd()
        {
            var initialCursorCol = _console.CursorLeft;

            new List<ConsoleKeyInfo>() { LeftArrow, CtrlT }
                .ForEach(_sut.Handle);

            Assert.AreEqual("Helol", _sut.Text);
            Assert.AreEqual(initialCursorCol, _console.CursorLeft);
        }

        [Test]
        public void TestControlT_CursorInMiddleOfLine()
        {
            Enumerable
                .Repeat(LeftArrow, 3)
                .ToList()
                .ForEach(_sut.Handle);

            var initialCursorCol = _console.CursorLeft;

            _sut.Handle(CtrlT);

            Assert.AreEqual("Hlelo", _sut.Text);
            Assert.AreEqual(initialCursorCol + 1, _console.CursorLeft);
        }

        [Test]
        public void TestControlT_CursorAtBeginningOfLine_HasNoEffect()
        {
            _sut.Handle(CtrlA);

            var initialCursorCol = _console.CursorLeft;

            _sut.Handle(CtrlT);

            Assert.AreEqual("Hello", _sut.Text);
            Assert.AreEqual(initialCursorCol, _console.CursorLeft);
        }

        [Test]
        public void TestHome()
        {
            new List<ConsoleKeyInfo>() { Home, 'S'.ToConsoleKeyInfo() }
                .ForEach(_sut.Handle);

            Assert.AreEqual("SHello", _sut.Text);
        }

        [Test]
        public void TestControlA()
        {
            new List<ConsoleKeyInfo>() { CtrlA, 'S'.ToConsoleKeyInfo() }
                .ForEach(_sut.Handle);

            Assert.AreEqual("SHello", _sut.Text);
        }

        [Test]
        public void TestEnd()
        {
            new List<ConsoleKeyInfo>() { Home, End, ExclamationPoint }
                .ForEach(_sut.Handle);

            Assert.AreEqual("Hello!", _sut.Text);
        }

        [Test]
        public void TestControlE()
        {
            new List<ConsoleKeyInfo>() { CtrlA, CtrlE, ExclamationPoint }
                .ForEach(_sut.Handle);

            Assert.AreEqual("Hello!", _sut.Text);
        }

        [Test]
        public void TestLeftArrow()
        {
            " N".Select(c => c.ToConsoleKeyInfo())
                .Prepend(LeftArrow)
                .ToList()
                .ForEach(_sut.Handle);

            Assert.AreEqual("Hell No", _sut.Text);
        }

        [Test]
        public void TestControlB()
        {
            " N".Select(c => c.ToConsoleKeyInfo())
                .Prepend(CtrlB)
                .ToList()
                .ForEach(_sut.Handle);

            Assert.AreEqual("Hell No", _sut.Text);
        }

        [Test]
        public void TestRightArrow()
        {
            new List<ConsoleKeyInfo>() { LeftArrow, RightArrow, ExclamationPoint }
                .ForEach(_sut.Handle);

            Assert.AreEqual("Hello!", _sut.Text);
        }

        [Test]
        public void TestControlD()
        {
            Enumerable.Repeat(LeftArrow, 4)
                    .Append(CtrlD)
                    .ToList()
                    .ForEach(_sut.Handle);

            Assert.AreEqual("Hllo", _sut.Text);
        }

        [Test]
        public void TestControlF()
        {
            new List<ConsoleKeyInfo>() { LeftArrow, CtrlF, ExclamationPoint }
                .ForEach(_sut.Handle);

            Assert.AreEqual("Hello!", _sut.Text);
        }

        [Test]
        public void TestControlL()
        {
            _sut.Handle(CtrlL);
            Assert.AreEqual(string.Empty, _sut.Text);
        }

        [Test]
        public void TestUpArrow()
        {
            _history.AsEnumerable().Reverse().ToList().ForEach((history) => {
                _sut.Handle(UpArrow);
                Assert.AreEqual(history, _sut.Text);
            });
        }

        [Test]
        public void TestControlP()
        {
            _history.AsEnumerable().Reverse().ToList().ForEach((history) => {
                _sut.Handle(CtrlP);
                Assert.AreEqual(history, _sut.Text);
            });
        }

        [Test]
        public void TestDownArrow()
        {
            Enumerable.Repeat(UpArrow, _history.Count)
                    .ToList()
                    .ForEach(_sut.Handle);

            _history.ForEach(history => {
                Assert.AreEqual(history, _sut.Text);
                _sut.Handle(DownArrow);
            });
        }

        [Test]
        public void TestControlN()
        {
            Enumerable.Repeat(UpArrow, _history.Count)
                    .ToList()
                    .ForEach(_sut.Handle);

            _history.ForEach(history => {
                Assert.AreEqual(history, _sut.Text);
                _sut.Handle(CtrlN);
            });
        }

        [Test]
        public void TestControlU()
        {
            _sut.Handle(LeftArrow);
            _sut.Handle(CtrlU);

            Assert.AreEqual("o", _sut.Text);

            _sut.Handle(End);
            _sut.Handle(CtrlU);

            Assert.AreEqual(string.Empty, _sut.Text);
        }

        [Test]
        public void TestControlK()
        {
            _sut.Handle(LeftArrow);
            _sut.Handle(CtrlK);

            Assert.AreEqual("Hell", _sut.Text);

            _sut.Handle(Home);
            _sut.Handle(CtrlK);

            Assert.AreEqual(string.Empty, _sut.Text);
        }

        [Test]
        public void TestControlW()
        {
            " World".Select(c => c.ToConsoleKeyInfo())
                    .Append(CtrlW)
                    .ToList()
                    .ForEach(_sut.Handle);

            Assert.AreEqual("Hello ", _sut.Text);

            _sut.Handle(Backspace);
            _sut.Handle(CtrlW);

            Assert.AreEqual(string.Empty, _sut.Text);
        }

        [Test]
        public void TestTab()
        {
            _sut.Handle(Tab);
            // Nothing happens when no auto complete handler is set
            Assert.AreEqual("Hello", _sut.Text);

            _sut = new KeyHandler(new FakeConsole(), _history, _autoCompleteHandlerMock.Object);

            "Hi ".Select(c => c.ToConsoleKeyInfo()).ToList().ForEach(_sut.Handle);

            _completions.ToList().ForEach(completion => {
                _sut.Handle(Tab);
                Assert.AreEqual($"Hi {completion}", _sut.Text);
            });
        }

        [Test]
        public void TestBackwardsTab()
        {
            _sut.Handle(Tab);

            // Nothing happens when no auto complete handler is set
            Assert.AreEqual("Hello", _sut.Text);

            _sut = new KeyHandler(new FakeConsole(), _history, _autoCompleteHandlerMock.Object);

            "Hi ".Select(c => c.ToConsoleKeyInfo()).ToList().ForEach(_sut.Handle);

            // Bring up the first Autocomplete
            _sut.Handle(Tab);

            _completions.Reverse().ToList().ForEach(completion => {
                _sut.Handle(ShiftTab);
                Assert.AreEqual($"Hi {completion}", _sut.Text);
            });
        }

        [Test]
        public void MoveCursorThenPreviousHistory()
        {
            _sut.Handle(LeftArrow);
            _sut.Handle(UpArrow);

            Assert.AreEqual("clear", _sut.Text);
        }
    }
}
