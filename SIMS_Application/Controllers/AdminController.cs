using Microsoft.AspNetCore.Mvc;

namespace SIMS_Application.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult CrudAdmin()
        {
            return View();
        }

    }
}
