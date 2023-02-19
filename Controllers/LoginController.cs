using Milestone.Models;
using Milestone.Services;
using Microsoft.AspNetCore.Mvc;

namespace Milestone.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessLogin(UserModel user)
        {
            SecurityService securityService = new SecurityService();

            if(securityService.IsValid(user))
            {
                return View("/Views/Game/Index.cshtml");
            }
            else
            {
                return View("LoginFailure", user);
            }
        }
    }
}
