using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public ViewResult Index()
        {
            var model= _employeeRepository.GetAllEmployees();
            return View(model);
        }

        [Route("Home/Details/{id=1}")]
        public ViewResult details(int id)
        {
            Employee model = _employeeRepository.GetEmployee(id);
            ViewData["PageTitle"] = "Employee Details";
            //ViewData["Employee"] = model;
            return View(model);
        }
    }
}
