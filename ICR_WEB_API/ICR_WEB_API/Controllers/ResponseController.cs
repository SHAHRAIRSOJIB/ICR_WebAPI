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
    public class ResponseController : ControllerBase
    {
        private readonly IResponseRepo _responseRepo;
        public ResponseController(IResponseRepo responseRepo)
        {
            _responseRepo = responseRepo;
        }

        [HttpGet("GetAllFormated")]
        public async Task<IActionResult> GetAllFormated()
        {
            var data = await _responseRepo.GetAllFormatedResponse();
            if (data.Count > 0)
                return Ok(data);
            else
                return NotFound(new { message = "No Response Data found" });
        }

        [HttpGet("GetAll")]
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

        [HttpPost("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus(ResponseUpdateStatusDTO entity)
        {
            if (entity == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid data"
                });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ValidationState);
            }

            var response = await _responseRepo.GetByUnifiedLicenseNumber(entity.UnifiedLicenseNumber);

            if (response == null)
            {
                return NotFound(new
                {
                    Message = "User response not exist"
                });
            }

            var updatedResponse = await _responseRepo.UpdateStatus(entity.UnifiedLicenseNumber, entity.IsAnswerSubmitted);

            if (updatedResponse == null)
            {
                return BadRequest(new
                {
                    Message = "Unsuccessful response status update"
                });
            }

            return Ok(new
            {
                Message = "Successful response status update",
                Response = updatedResponse
            }
            );
        }

        [HttpPost]
        public async Task<IActionResult> Save(Response entity)
        {
            if (entity == null || entity.UserId <= 0)
            {
                return BadRequest(new
                {
                    Message = "Invalid data"
                });
            }

            var response = await _responseRepo.GetByUnifiedLicenseNumber(entity.UnifiedLicenseNumber);

            if (response != null && response.IsAnswerSubmitted == false)
            {
                return Ok(new
                {
                    Message = "Already exist but answers not submitted",
                    Response = response
                });
            }
            else if (response != null && response.IsAnswerSubmitted == true)
            {
                return BadRequest(new
                {
                    Message = "Already submitted"
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
