using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G01.BLL.Interfaces;
using Route.C41.G01.DAL.Models;
using System;

namespace Route.C41.G01.PL.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly IDepartmintRepository _departmintRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IUnitOfWork _unitOfWork;
        
        
        public DepartmentController(IUnitOfWork unitOfWork , IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
           _env = env;
        }

        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmintRepository.GetAll();

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
                 _unitOfWork.DepartmintRepository.Add(department);
                //if (count > 0)
                //{
                    return RedirectToAction(nameof(Index));
                //}
            }
                return View(department);

        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var department = _unitOfWork.DepartmintRepository.Get(id.Value);
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
            var department = _unitOfWork.DepartmintRepository.Get(id.Value);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    _unitOfWork.DepartmintRepository.Update(department);
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
        
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var department = _unitOfWork.DepartmintRepository.Get(id.Value);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);

        }
        [HttpPost]
        public IActionResult Delete(Department department)
        {
            try
            {
                _unitOfWork.DepartmintRepository.Delete(department);
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
            return View(department);
        }



    }
}
