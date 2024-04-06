using Microsoft.AspNetCore.Mvc;

namespace Route.C41.G01.PL.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
