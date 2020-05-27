using NUnit.Framework;
using OpenQA.Selenium;
using System.IO;
using System.Linq;
using System.Reflection;
using TableAutomation;

namespace TableAutomation
{
    [TestFixture]
    public class EmployeeTableTests : BaseTestClass
    {
        EmployeeTable _table;

        [SetUp]
        public void refreshEmployeeTable()
        {
            _driver.Navigate().Refresh();
            _table = new EmployeeTable(_driver);
        }

        [Test]
        public void SearchByFirstName()
        {
            var results = _table.getEmployee("FirstName", "Jean");
            IWebElement lastName = results.LastName;
            Assert.AreEqual(lastName.Text, "Gray");
        }

        [Test]
        public void SearchByLastName()
        {
            var results = _table.getEmployee("LastName", "Lebeau");
            IWebElement firstName = results.FirstName;
            Assert.AreEqual(firstName.Text, "Remy");
        }

    }
}
