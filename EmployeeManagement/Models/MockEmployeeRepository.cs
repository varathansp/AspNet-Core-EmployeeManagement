using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _EmployeeList;

        public MockEmployeeRepository()
        {
            _EmployeeList = new List<Employee>() {
                new Employee() { Id=1,Name="Varathan",Email="vara@gmail.com",Department=Dept.HR},
                new Employee() { Id=2,Name="Thenz",Email="thenz@gmail.com",Department=Dept.IT },
                new Employee() { Id=3,Name="Deep",Email="deep@gmail.com",Department=Dept.Payroll }
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _EmployeeList.Max(x => x.Id)+1;
            _EmployeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
           Employee employee= _EmployeeList.FirstOrDefault(x => x.Id == id);
            if (employee != null)
            {
                _EmployeeList.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _EmployeeList;
        }

        public Employee GetEmployee(int id)
        {
           return _EmployeeList.Where(x => x.Id == id).FirstOrDefault();
        }

        public Employee Update(Employee employeeChanges)
        {
            Employee employee = _EmployeeList.FirstOrDefault(x => x.Id == employeeChanges.Id);
            if (employee != null)
            {
                employee.Name = employeeChanges.Name;
                employee.Email = employeeChanges.Email;
                employee.Department = employeeChanges.Department;
            }
            return employee;
        }
    }
}
