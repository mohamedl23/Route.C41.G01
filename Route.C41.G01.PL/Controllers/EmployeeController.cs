using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G01.BLL.Interfaces;
using Route.C41.G01.BLL.Repcsitories;
using Route.C41.G01.DAL.Models;
using Route.C41.G01.DAL.Models.ViewModels;
using Route.C41.G01.PL.Mapper_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace Route.C41.G01.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmintRepository _departmintRepository;
        private readonly IWebHostEnvironment _env;

        //public EmployeeController(IEmployeeRepository EmployeeRepository, IWebHostEnvironment env)
        //{
        //    _employeeRepository = EmployeeRepository;
            
        //}

        public EmployeeController(IMapper mapper,IEmployeeRepository employeeRepository, IDepartmintRepository departmintRepository, IWebHostEnvironment env)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _departmintRepository = departmintRepository;
            _env = env;
        }

        public IActionResult Index(string SearchInput)
        {
            IEnumerable<Employee> employees;
            ///Binding is One Way Binding in MVC
            /// View Data Is A Dictionary Object
            ///ViewData["Message"] = "Hello View Data";
            ///ViewBag.Message = "Hello View Bag";
            ///IEnumerable<Employee> employees;
            //var employees = Enumerable.Empty<Employee>();
            

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

            var EmpMapped = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees) ;
            return View( EmpMapped);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = _departmintRepository.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                employeeVM.ImageName = DocumentSettings.UploadFiles(employeeVM.Image, "Images");
                var MappedEmp = _mapper.Map<EmployeeViewModel , Employee>(employeeVM);
                var count = _employeeRepository.Add(MappedEmp);
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
            return View(employeeVM);

        }

        public IActionResult Details(int? id ,string ViewName = "Details" )
        {
            if (id == null)
            {
                return BadRequest();
            }
            var employees = _employeeRepository.Get(id.Value);
            if (employees == null)
            {
                return NotFound();
            }
            var MappedEmp = _mapper.Map<Employee,EmployeeViewModel>(employees);
            return View(ViewName , MappedEmp);
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
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _employeeRepository.Update(MappedEmp);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // 1. Log Exption
                    // 2. Friendly Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeeVM);
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
        public IActionResult Delete(EmployeeViewModel employeeVM)
        {
            try
            {
                var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _employeeRepository.Delete(MappedEmp);
                DocumentSettings.DeleteFile(employeeVM.ImageName, "Images");
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
            return View(employeeVM);
        }

       

    }
}
