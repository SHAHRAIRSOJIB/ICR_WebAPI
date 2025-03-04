using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.BLL.Services;
using ICR_WEB_API.Service.Entity;
using ICR_WEB_API.Service.Model.DTOs;
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

        [HttpGet("{id}")]
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
        public async Task<IActionResult> Save(Response entity)
        {
            if (entity == null || entity.Id == 0)
            {
                return BadRequest(new
                {
                    Message = "Invalid data"
                });
            }

            bool isExist = await _responseRepo.IsExist(entity);

            if (isExist)
            {
                return BadRequest(new
                {
                    Message = "Already exist"
                });
            }

            var result = await _responseRepo.Save(entity);

            if (result == null)
            {
                return BadRequest(new
                {
                    Message = "Unsuccessful Save"
                });
            }

            return Ok(result);
        }
    }
}
