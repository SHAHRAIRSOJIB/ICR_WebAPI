using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ICR_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;

        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        [HttpGet]
        public async Task<List<User>> GetAllQuestion()
        {
            var list = await _userRepo.GetAll();
            return list;
        }

        [HttpPost("saveUser")]

        public async Task<int> SaveUser(User entity)
        {
            return await _userRepo.SaveUser(entity);
        }

        [HttpPut]
        public async Task<IActionResult> Update(User entity)
        {
            if (entity == null || entity.Id == 0)
            {
                return BadRequest(new
                {
                    Message = "Invalid data"
                });
            }

            var isExits = await _userRepo.IsExist(entity);

            if (!isExits)
            {
                return NotFound(new
                {
                    Message = "User Not found"
                });
            }

            var updatedUser = await _userRepo.Update(entity);

            if (updatedUser == null)
            {
                return BadRequest(new
                {
                    Message = "Unsuccessful User Update"
                });
            }

            return Ok(updatedUser);
        }
        [HttpPost("saveAdmin")]
        public async Task<int> SaveAdmin(User entity)
        {
            return await _userRepo.SaveAdmin(entity);
        }
        [HttpPost("resetPassword")]
        //[Authorize]
        public async Task<string> ResetPassword(ForgetPasswordDTO resetPasswordDTO)
        {
            return await _userRepo.ForgetPassword(resetPasswordDTO);
        }

    }
}
