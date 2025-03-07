using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.Model.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ICR_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepo _userRepo;

        public AuthController(IAuthService authService, IUserRepo userRepo)
        {
            _authService = authService;
            _userRepo = userRepo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var result = await _authService.AuthenticateUser(userName, password);

            if (result.Token != null)
            {
                return Ok(result);
            }
            else
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
        }
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDTO entity)
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

            var isExits = await _userRepo.IsExistByEmail(entity.Email);

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

        //[Authorize]
        //[HttpPost("ForgetPassword")]
        //public async Task<IActionResult> ForgetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState.ValidationState);
        //    }

        //    var response = await _userRepo.ResetPassword(resetPasswordDTO);
        //    return Ok(response);
        //}
    }
}
