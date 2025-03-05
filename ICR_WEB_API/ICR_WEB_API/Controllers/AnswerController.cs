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
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerRepo _answerRepo;

        public AnswerController(IAnswerRepo answerRepo)
        {
            _answerRepo = answerRepo;
        }
        [HttpGet]
        public async Task<List<AnswerDTO>> GetAllQuestion()
        {
            var list = await _answerRepo.GetAll();
            return list;
        }

        [HttpPost]
        public async Task<IActionResult> Save(List<Answer> entities)
        {
            if (entities == null) {
                return BadRequest(new
                {
                    Message = "Invalid data"
                });
            }

            if(await _answerRepo.Save(entities) > 0)
                return Ok(new
                {
                    Message = "Successful Submit answers"
                });
            else
                return BadRequest(new
                {
                    Message = "Unsuccessful Submit answers"
                }); ;
        }

        [HttpPost("Update")]
        public async Task<string> Update(Answer entity)
        {
            return await _answerRepo.Update(entity);
        }
    }
}
