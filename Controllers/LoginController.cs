using Milestone.Models;
using Milestone.Services;
using Microsoft.AspNetCore.Mvc;
using Activity_2_RegisterAndLoginApp.Utility;

namespace Milestone.Controllers
{
    public class LoginController : Controller
    {

        public ILoggers Logger { get; set; }

        public LoginController(ILoggers loggers)
        {
            Logger = loggers;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessLogin(UserModel user)
        {
            Logger.Info("Entering The ProcessLogin Method");
            SecurityService securityService = new SecurityService();

            if(securityService.IsValid(user))
            {
				HttpContext.Session.SetString("username", user.UserName);
                Logger.Info($"{user.UserName} is Valid");
				return RedirectToAction("Index", "game");
            }
            else
            {
                Logger.Error("Login Failed.");
				return View("LoginFailure", user);
            }
        }
    }
}
