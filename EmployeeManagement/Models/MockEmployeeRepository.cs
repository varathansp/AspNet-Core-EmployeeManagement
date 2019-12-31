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
                new Employee() { Id=1,Name="Varathan",Email="vara@gmail.com",Department="Apps" },
                new Employee() { Id=2,Name="Thenz",Email="thenz@gmail.com",Department="Apps" },
                new Employee() { Id=3,Name="Deep",Email="deep@gmail.com",Department="Apps" }
            };
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _EmployeeList;
        }

        public Employee GetEmployee(int id)
        {
           return _EmployeeList.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
