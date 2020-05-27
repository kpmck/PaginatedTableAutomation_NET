using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static TableAutomation.EmployeeTable;

namespace TableAutomation
{
    public class EmployeeTable
    {

        public IWebDriver _driver;
        public By employeeTable = By.Id("myTable");
        public By nextPage = By.ClassName("next_link");
        public By headers = By.XPath("./ancestor::*//th");
        public By rows = By.XPath("./tr[not(contains(@style,'none'))]");

        public EmployeeTable(IWebDriver driver)
        {
            _driver = driver;
        }

        public class Employee
        {
            public IWebElement Checkbox { get; set; }
            public IWebElement ID { get; set; }
            public IWebElement FirstName { get; set; }
            public IWebElement LastName { get; set; }
            public IWebElement Job { get; set; }
        }

        public Employee getEmployee(String columnName, String columnValue)
        {
            return searchEmployeeTable(columnName, columnValue).FirstOrDefault();
        }

        private List<Employee> searchEmployeeTable(String columnName, String columnValue)
        {
            return queryDataTable(employeeTable, nextPage, columnName, columnValue);
        }

        private List<Employee> queryDataTable(By employeetable, By nextPage, String columnName, String columnValue)
        {
            List<Employee> tableData = getTableData(employeeTable);
            IWebElement nextPageButton = _driver.FindElement(nextPage);
            List<Employee> results = parseResults(tableData, columnName, columnValue);

            while (!results.Any())
            {
                tableData = getTableData(employeeTable);
                results = parseResults(tableData, columnName, columnValue);
                if (results.Any() || !nextPageButton.Displayed)
                    break;
                nextPageButton.Click();
            }
            return results;
        }

        private List<Employee> getTableData(By employeeTable)
        {
            List<Employee> employees = new List<Employee>();
            IList<IWebElement> tableRows = _driver.FindElement(employeeTable).FindElements(rows);

            for (int i = 0; i < tableRows.Count; i++)
            {
                IList<IWebElement> rowCells = tableRows[i].FindElements(By.TagName("td"));
                Employee employee = new Employee();
                for (int j = 0; j < rowCells.Count; j++)
                {
                    employee.Checkbox = rowCells[0];
                    employee.ID = rowCells[1];
                    employee.FirstName = rowCells[2];
                    employee.LastName = rowCells[3];
                    employee.Job = rowCells[4];
                }
                employees.Add(employee);
            }
            return employees;
        }

        private List<Employee> parseResults(List<Employee> tableData, String columnName, String columnValue)
        {
            switch (columnName.ToLower())
            {
                case ("id"):
                    return tableData.Where(x => x.ID.Text.Equals(columnValue)).ToList();
                case ("firstname"):
                    return tableData.Where(x => x.FirstName.Text.Equals(columnValue)).ToList();
                case ("lastname"):
                    return tableData.Where(x => x.LastName.Text.Equals(columnValue)).ToList();
                case ("job"):
                    return tableData.Where(x => x.Job.Text.Equals(columnValue)).ToList();
                default:
                    throw new Exception(String.Format("Supplied column name %s does not exist for the table.", columnName));
            }
        }
    }
}

