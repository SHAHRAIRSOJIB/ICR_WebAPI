using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.Entity;
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
        public async Task<List<Answer>> GetAllQuestion()
        {
            var list = await _answerRepo.GetAll();
            return list;
        }

        [HttpPost]
        public async Task<int> Save(Answer entity)
        {
            return await _answerRepo.Save(entity);
        }

        [HttpPut]
        public async Task<string> Update(Answer entity)
        {
            return await _answerRepo.Update(entity);
        }
    }
}
