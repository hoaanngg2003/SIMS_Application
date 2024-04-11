using Microsoft.AspNetCore.Mvc;

namespace SIMS_Application.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult ProfileIndex()
        {
            return View();
        }
    }
}
