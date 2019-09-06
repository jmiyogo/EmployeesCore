using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesCore
{
    public class Employees
    {

        private List<Employee> EmployeeList;

        public Employees(String CSVData)
        {
            ValidateCSVData(CSVData);
            EmployeeList = GetEmployeesFromCSVDataString(CSVData);
            ValidateAllManagersAreEmployees(EmployeeList);
            ValidateCEO(EmployeeList);
        }

        #region 1. Validation
        public void ValidateCSVData(String CSVData)
        {
            if (string.IsNullOrEmpty(CSVData))
            {
                throw new NullReferenceException("Invalid CSV");
            }
        }

        private List<Employee> GetEmployeesFromCSVDataString(string CSVData)
        {
            List<Employee> employees = new List<Employee>();

            string[] rows = CSVData.Split('\n');
            foreach (string row in rows)
            {
                string[] columns = row.Split(',');
                var employee = new Employee
                {
                    EmployeeId = columns[0],
                    ManagerId = columns[1],
                    Salary = int.Parse(columns[2]) // this will throw an exception if the data is not an integer
                };
                employees.Add(employee);
            }

            return employees;
        }

        private void ValidateCEO(List<Employee> employees)
        {
            var ceo = employees.Single(p => string.IsNullOrEmpty(p.ManagerId));
        }

        private void ValidateAllManagersAreEmployees(List<Employee> employees)
        {
            foreach (var employee in employees)
            {
                if (!string.IsNullOrEmpty(employee.ManagerId))
                {
                    var manager = employees.Single(p => p.EmployeeId == employee.ManagerId);
                }
            }
        }

        private void ValidateEmployeeHierarchy(List<Employee> employees)
        {
            foreach (var i in employees)
            {
                var manager = employees.Single(p => p.EmployeeId == i.EmployeeId); //checking for duplicates
            }
        }

        #endregion

        #region 1. Compute Salary
        public long GetManagerSalaryBudget(string managerId)
        {
            long salaryBudget = 0;

            var managerEmployees = EmployeeList.Where(p => p.ManagerId == managerId);
            if (managerEmployees.Any()) //If the employee is not a manager there is no salary budget
            {
                //Manager Employees Salary
                foreach (var managerEmployee in managerEmployees)
                {
                    salaryBudget += managerEmployee.Salary;
                }

                //Manager Salary
                var managerSalary = EmployeeList.FirstOrDefault(p => p.EmployeeId == managerId);
                if (managerSalary != null)
                {
                    salaryBudget += managerSalary.Salary;
                }
            }

            return salaryBudget;
        }
        #endregion
    }

}
