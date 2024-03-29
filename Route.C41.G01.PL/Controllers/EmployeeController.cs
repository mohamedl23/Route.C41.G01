using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G01.BLL.Interfaces;
using Route.C41.G01.BLL.Repcsitories;
using Route.C41.G01.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Route.C41.G01.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmintRepository _departmintRepository;
        private readonly IWebHostEnvironment _env;

        //public EmployeeController(IEmployeeRepository EmployeeRepository, IWebHostEnvironment env)
        //{
        //    _employeeRepository = EmployeeRepository;
            
        //}

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmintRepository departmintRepository, IWebHostEnvironment env)
        {
            _employeeRepository = employeeRepository;
            _departmintRepository = departmintRepository;
            _env = env;
        }

        public IActionResult Index(string SearchInput)
        {
            //Binding is One Way Binding in MVC
            // View Data Is A Dictionary Object

            //ViewData["Message"] = "Hello View Data";
            //ViewBag.Message = "Hello View Bag";
            //IEnumerable<Employee> employees;
            var employees = Enumerable.Empty<Employee>();

            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = _employeeRepository.GetAll();
            }
            else
            {
                employees = _employeeRepository.SearchByName(SearchInput.ToLower());
            }

            var departments = _employeeRepository.GetAll();
            
            ViewBag.Departments = _departmintRepository.GetAll();

            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = _departmintRepository.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var count = _employeeRepository.Add(employee);
                if (count > 0)
                {
                    TempData["Message"] = "Employee is Created Successfully";
                }
                else
                {
                    TempData["Message"] = "An Error Has Occured , Department Not Created";
                }
                    return RedirectToAction(nameof(Index));
            }
            return View(employee);

        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var employee = _employeeRepository.Get(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        public IActionResult Edit(int? id)
        {
            ViewBag.Departments = _departmintRepository.GetAll();

            if (id == null)
            {
                return BadRequest();
            }
            var employee = _employeeRepository.Get(id.Value);

            if (employee == null)
            {
                return NotFound();
            }
            
            return View(employee);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeRepository.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // 1. Log Exption
                    // 2. Friendly Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employee);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var employee = _employeeRepository.Get(id.Value);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);

        }
        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            try
            {
                _employeeRepository.Delete(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error");
                }
            }
            return View(employee);
        }

       
    }
}
