using Microsoft.AspNetCore.Mvc;

namespace SIMS_Application.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
