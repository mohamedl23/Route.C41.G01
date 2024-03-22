using Microsoft.AspNetCore.Mvc;
using Route.C41.G01.BLL.Interfaces;

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
            return View();
        }
    }
}
