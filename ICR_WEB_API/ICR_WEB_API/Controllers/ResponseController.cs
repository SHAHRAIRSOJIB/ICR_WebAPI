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
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _responseRepo.GetAll();
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetResponseById(int id)
        {
            var data = await _responseRepo.GetById(id);
            if (data != null && data.Id != 0)
            {
                return Ok(data);
            }

            return NotFound(new { message = "Data not found" });
        }

        [HttpPost]
        public async Task<int> Save(Response entity)
        {
            return await _responseRepo.Save(entity);
        }
    }
}
