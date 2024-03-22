using Microsoft.AspNetCore.Mvc;
using Route.C41.G01.BLL.Interfaces;
using Route.C41.G01.DAL.Models;

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

    }
}
