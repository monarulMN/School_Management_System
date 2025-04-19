using Microsoft.AspNetCore.Mvc;

namespace SchoolProjectWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GradesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
