using Microsoft.AspNetCore.Mvc;
using SchoolProject.Services;
using SchoolProject.Utilities;
using SchoolProject.ViewModels;

namespace SchoolProjectWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SessionsController : Controller
    {
        private ISessionService _sessionService;

        public SessionsController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            PagedResult<SessionViewModel> session = _sessionService.GetAll(pageNumber, pageSize);
            return View(session);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateSessionViewModel viewModel = new CreateSessionViewModel();
            viewModel.Start = DateTime.Now.Year.ToString();
            var afterYear = DateTime.Now.AddYears(1);
            viewModel.End = afterYear.Year.ToString();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSessionViewModel viewModel)
        {
            if (viewModel == null)
            {
                return View();
            }
            await _sessionService.Add(viewModel);
            return RedirectToAction("Index");
        }
    }
}
