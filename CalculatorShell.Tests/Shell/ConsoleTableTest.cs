using CalculatorShell.Ui;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace CalculatorShell.Tests.Shell
{
    [TestFixture]
    public class ConsoleTableTest
    {
        [Test]
        public void ShouldBeToStringFromList()
        {
            var users = new List<User>
            {
                new User { Name = "Alexandre" , Age = 36 }
            };
            var table = ConsoleTable.From(users).ToString();

            Assert.AreEqual(
$@" ------------------- 
 | Name      | Age |
 ------------------- 
 | Alexandre | 36  |
 ------------------- 

 Count: 1", table);
        }

        [Test]
        public void ShouldBeAvoidErrorOnToStringFromAddRows()
        {
            var table = new ConsoleTable("one", "two", "three")
                .AddRow(1, 2, 3)
                .AddRow("this line should be longer", "yes it is", "oh")
                .Configure(o => o.NumberAlignment = CellAlignment.Right)
                .ToString();

            Assert.AreEqual(
$@" -------------------------------------------------- 
 | one                        | two       | three |
 -------------------------------------------------- 
 | 1                          | 2         | 3     |
 -------------------------------------------------- 
 | this line should be longer | yes it is | oh    |
 -------------------------------------------------- 

 Count: 2", table);
        }

        [Test]
        public void NumberShouldBeRightAligned()
        {
            var users = new List<User>
            {
                new User { Name = "Alexandre" , Age = 36 }
            };
            var table = ConsoleTable
                .From(users)
                .Configure(o => o.NumberAlignment = CellAlignment.Right)
                .ToString();

            Assert.AreEqual(
$@" ------------------- 
 | Name      | Age |
 ------------------- 
 | Alexandre |  36 |
 ------------------- 

 Count: 1", table);
        }

        [Test]
        public void NumberShouldBeRightAlignedOnMarkDown()
        {
            var users = new List<User>
            {
                new User { Name = "Alexandre" , Age = 36 }
            };
            var table = ConsoleTable
                .From(users)
                .Configure(o => o.NumberAlignment = CellAlignment.Right)
                .ToMarkDownString();

            Assert.AreEqual(
$@"| Name      | Age |
|-----------|-----|
| Alexandre |  36 |
", table);
        }

        [Test]
        public void OutputShouldDefaultToConsoleOut()
        {
            var users = new List<User>
            {
                new User { Name = "Alexandre" , Age = 36 }
            };

            var table = ConsoleTable.From(users);

            Assert.AreEqual(table.Options.OutputTo, Console.Out);
        }

        [Test]
        public void OutputShouldGoToConfiguredOutputWriter()
        {
            var users = new List<User>
            {
                new User { Name = "Alexandre" , Age = 36 }
            };

            var testWriter = new StringWriter();

            ConsoleTable
               .From(users)
               .Configure(o => o.OutputTo = testWriter)
               .Write();

            Assert.IsNotEmpty(testWriter.ToString());
        }

        class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
