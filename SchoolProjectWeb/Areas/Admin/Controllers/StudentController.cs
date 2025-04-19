using Microsoft.AspNetCore.Mvc;
using SchoolProject.Services;
using SchoolProject.ViewModels;

namespace SchoolProjectWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentController : Controller
    {
        private IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Addstudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(CreateStudentViewModel viewModel)
        {
            await _studentService.AddStudent(viewModel);
            return RedirectToAction("Index");
        }
    }
}
