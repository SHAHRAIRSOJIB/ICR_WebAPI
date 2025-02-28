using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.Entity;
using ICR_WEB_API.Service.Model.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ICR_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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

        [HttpPost]
        public async Task<int> Save(Question entity)
        {
            return await _questionRepo.Save(entity);
        }

        [HttpPut]
        public async Task<string> Update(Question entity)
        {
            return await _questionRepo.Update(entity);
        }


    }
}
