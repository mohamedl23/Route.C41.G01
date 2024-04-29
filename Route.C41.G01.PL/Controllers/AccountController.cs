using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Route.C41.G01.DAL.Models;
using Route.C41.G01.PL.ViewModels.User;
using System.Threading.Tasks;

namespace Route.C41.G01.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user is null)
                {
                    user = new ApplicationUser()
                    {
                        FName = model.FirstName,
                        LName = model.LastName,
                        UserName = model.Username,
                        Email = model.Email,
                        IsAgree = model.ISAgree
                    };

					var result = await _userManager.CreateAsync(user, model.Password);
					if (result.Succeeded)
					{
						return RedirectToAction(nameof(SignIn));
					}
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

				}
				ModelState.AddModelError(string.Empty, "This User Is Already In Use For Another Account");

				

				    
			}
            
            return View();
        }
    }
}
