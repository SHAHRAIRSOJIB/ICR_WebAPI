using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICR_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ResponseController : ControllerBase
    {
        private readonly IResponseRepo _responseRepo;
        public ResponseController(IResponseRepo responseRepo)
        {
            _responseRepo = responseRepo;
        }
        [HttpGet]
        public async Task<Response> GetResponseById(int id)
        {
            var data = await _responseRepo.GetById(id);
            return data;
        }

        [HttpPost]
        public async Task<int> Save(Response entity)
        {
            return await _responseRepo.Save(entity);
        }
    }
}
