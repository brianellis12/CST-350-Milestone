using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using Milestone.Services;

namespace Activity_2_RegisterAndLoginApp.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessRegistration(UserModel user)
        {
            SecurityService securityService = new SecurityService();

            if (securityService.AddUser(user))
            {
                return RedirectToAction("Index", "game");
            }
            else
            {
                return View("RegistrationFailure", user);
            }
        }
    }
}
