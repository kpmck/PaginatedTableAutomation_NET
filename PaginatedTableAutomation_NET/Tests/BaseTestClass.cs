using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;

namespace TableAutomation
{
    public class BaseTestClass
    {
        public static IWebDriver _driver;
        public static string _applicationPath;

        [OneTimeSetUp]
        public static void Setup()
        {
            _applicationPath = string.Concat(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                            "\\TableApplication\\EmployeeTable.html");
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(_applicationPath);
        }

        [OneTimeTearDown]
        public static void TearDown()
        {
            _driver.Quit();
        }

    }
}