using System;
using EmployeesCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmployeesTests
{
    [TestClass]
    public class UnitTest1
    {
        string validCSVData = "Employee4,Employee2,500\nEmployee3,Employee1,800\nEmployee1,,1000\nmployee5,Employee1,500\nEmployee2,Employee1,500";
        string inValidCSVData_CEO = "Employee4,Employee2,500\nEmployee3,Employee1,800\nEmployee3,,1000\nmployee5,Employee1,500\nEmployee2,Employee1,500";
        string inValidCSVData_EmployeeSalary = "Employee4,Employee2,A\nEmployee3,Employee1,800\nEmployee3,,1000\nmployee5,Employee1,500\nEmployee2,Employee1,500";
        string inValidCSVData_AllManagersAreEmployees = "Employee4,Employee2,500\nEmployee1,Employee1,800\nEmployee3,Employee7,1000\nmployee5,Employee1,500\nEmployee2,Employee1,500";

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestEmployeesConstructor_InvalidCSVData()
        {
            var employees = new Employees(null);
        }

        [TestMethod]
        public void TestEmployeesConstructor_ValidCSVData()
        {
            var employees = new Employees(validCSVData);
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestEmployeesConstructor_InValidEmployeeSalary()
        {
            var employees = new Employees(inValidCSVData_EmployeeSalary);
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestEmployeesConstructor_InValidCEO()
        {
            var employees = new Employees(inValidCSVData_CEO);
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestEmployeesConstructor_InValidAllManagersAreEmployees()
        {
            var employees = new Employees(inValidCSVData_AllManagersAreEmployees);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestEmployeesGetManagerSalaryBudget()
        {
            var employees = new Employees(validCSVData);
            var managerBudget = employees.GetManagerSalaryBudget("Employee1");
            Assert.AreEqual(managerBudget, 2800);
        }

        [TestMethod]
        public void TestEmployeesGetManagerSalaryBudget_Invalid()
        {
            var employees = new Employees(validCSVData);
            var managerBudget = employees.GetManagerSalaryBudget("Employee2");
            Assert.AreNotEqual(managerBudget, 0); //Employee2 expected budget is 1000
        }



    }
}
