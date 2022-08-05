using CalculatorShell.Ui;
using NUnit.Framework;

namespace CalculatorShell.Tests.Shell
{
    [TestFixture]
    public class ConsoleTableTest
    {
        [Test]
        public void ShouldBeToStringFromList()
        {
            List<User> users = new List<User>
            {
                new User { Name = "Alexandre" , Age = 36 }
            };
            string table = ConsoleTable.From(users).ToString();

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
            string table = new ConsoleTable("one", "two", "three")
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
            List<User> users = new List<User>
            {
                new User { Name = "Alexandre" , Age = 36 }
            };
            string table = ConsoleTable
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
            List<User> users = new List<User>
            {
                new User { Name = "Alexandre" , Age = 36 }
            };
            string table = ConsoleTable
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
            List<User> users = new List<User>
            {
                new User { Name = "Alexandre" , Age = 36 }
            };

            ConsoleTable table = ConsoleTable.From(users);

            Assert.AreEqual(table.Options.OutputTo, Console.Out);
        }

        [Test]
        public void OutputShouldGoToConfiguredOutputWriter()
        {
            List<User> users = new List<User>
            {
                new User { Name = "Alexandre" , Age = 36 }
            };

            StringWriter testWriter = new StringWriter();

            ConsoleTable
               .From(users)
               .Configure(o => o.OutputTo = testWriter)
               .Write();

            Assert.IsNotEmpty(testWriter.ToString());
        }

        private class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
