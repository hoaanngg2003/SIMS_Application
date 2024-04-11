using Microsoft.AspNetCore.Mvc;

namespace SIMS_Application.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult CourseIndex()
        {
            return View();
        }
        public IActionResult CourseDetail()
        {
            return View();
        }
    }
}
