using ICR_WEB_API.Service.BLL.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ICR_DashBoard.Controllers
{
    public class AnswerController : Controller
    {
        private readonly IAnswerRepo _answerRepo;
        public AnswerController(IAnswerRepo answerRepo)
        {
            _answerRepo = answerRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Report()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAnswerReport()
        {
            var answers = await _answerRepo.GetAll();
            return Json(answers);
        }
    }
}
