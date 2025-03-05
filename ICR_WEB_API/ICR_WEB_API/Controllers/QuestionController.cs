using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.Entity;
using ICR_WEB_API.Service.Model.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICR_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepo _questionRepo;

        public QuestionController(IQuestionRepo questionRepo)
        {
            _questionRepo = questionRepo;
        }

        [HttpGet]
        public async Task<List<QuestionDTO>> GetAllQuestion()
        {
            var list = await _questionRepo.GetAll();
            return list;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest(new { Message = "Inavlid Id" });
            }

            var question = await _questionRepo.GetAsync(id);

            if (question == null)
            {
                return NotFound(new { Message = "Question not found" });
            }

            return Ok(question);
        }

        [HttpPost]
        public async Task<int> Save(Question entity)
        {
            return await _questionRepo.Save(entity);
        }

        [HttpPost("Update")]
        public async Task<string> Update(Question entity)
        {
            return await _questionRepo.Update(entity);
        }
    }
}
