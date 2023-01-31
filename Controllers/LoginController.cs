using Activity_2_RegisterAndLoginApp.Models;
using Activity_2_RegisterAndLoginApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Activity_2_RegisterAndLoginApp.Controllers
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
                return View("LoginSuccess", user);
            }
            else
            {
                return View("LoginFailure", user);
            }
        }
    }
}
