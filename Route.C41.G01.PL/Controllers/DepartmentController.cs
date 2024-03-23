using Microsoft.AspNetCore.Mvc;
using Route.C41.G01.BLL.Interfaces;
using Route.C41.G01.DAL.Models;
using System;

namespace Route.C41.G01.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmintRepository _departmintRepository;
        public DepartmentController(IDepartmintRepository departmintRepository)
        {
            _departmintRepository = departmintRepository;
        }

        public IActionResult Index()
        {
            var departments = _departmintRepository.GetAll();

            return View(departments);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                var count = _departmintRepository.Add(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
                return View(department);

        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var department = _departmintRepository.Get(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            return View (department);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var department = _departmintRepository.Get(id.Value);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
            
        }
        [HttpPost]
        public IActionResult Edit([FromRoute]int id , Department department)
        {
            if ( id != department.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(department);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _departmintRepository.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex) 
                {
                    // 1. Log Exption
                    // 2. Friendly Message
                    ModelState.AddModelError(string.Empty , ex.Message );
                }
            }
            return View(department);
        }

    }
}
