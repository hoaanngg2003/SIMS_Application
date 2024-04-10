using Microsoft.AspNetCore.Mvc;

namespace SIMS_Application.Controllers
{
    public class WelcomeController : Controller
    {
        public IActionResult WelCome()
        {
            return View();
        }

    }
}
