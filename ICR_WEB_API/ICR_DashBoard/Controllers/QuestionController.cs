using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ICR_DashBoard.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionRepo _questionRepo;
        public QuestionController(IQuestionRepo questionRepo)
        {
            _questionRepo = questionRepo;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _questionRepo.GetAll();
            ViewBag.Questions = list;
            return View();
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Question question)
        {
            try
            {
                if (question != null)
                {
                    int savedId = await _questionRepo.Save(question);
                    if (savedId > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest(new { message = "Failed to create the question" });
                    }
                }
                else
                {
                    return BadRequest(new { message = "Invalid question data" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request", error = ex.Message });
            }
        }
    }
}
