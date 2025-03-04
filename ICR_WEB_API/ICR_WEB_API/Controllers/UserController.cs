using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.Entity;
using ICR_WEB_API.Service.Model.DTOs;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<List<User>> GetAllUser()
        {
            var list = await _userRepo.GetAll();
            return list;
        }

        [HttpPost("saveUser")]

        public async Task<IActionResult> SaveUser(UserDTO entity)
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

            var isExits = await _userRepo.IsExistByEmail(entity);

            if (isExits)
            {
                return NotFound(new
                {
                    Message = "User already exists"
                });
            }

            var saveUser = await _userRepo.SaveUser(entity);

            if (saveUser == null)
            {
                return BadRequest(new
                {
                    Message = "Unsuccessful User Registration"
                });
            }

            return Ok(saveUser);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(User entity)
        {
            if (entity == null || entity.Id == 0)
            {
                return BadRequest(new
                {
                    Message = "Invalid data"
                });
            }

            if (entity.UserType == null)
            {
                return BadRequest(new
                {
                    Message = "UserType is not defined"
                });
            }

            if (string.IsNullOrEmpty(entity.Email))
            {
                return BadRequest(new
                {
                    Message = "Email is not defined"
                });
            }

            var isExits = await _userRepo.IsExistById(entity);

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

        [Authorize]
        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            if (resetPasswordDTO == null)
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

            var result = await _userRepo.ResetPassword(resetPasswordDTO);

            if (result == null)
            {
                return BadRequest(new
                {
                    Message = "Unsuccessful Reset Password. Maybe Current password is wrong"
                });
            }

            return Ok(new
            {
                Message = "Password Change Successfull",
                User = result
            });
        }
    }
}
