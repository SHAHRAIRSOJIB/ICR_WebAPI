using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.Entity;
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

            if (result == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            else {
                return Ok(result);
            }
        }
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            var result = await _userRepo.SaveUser(user);

            if (result == null)
            {
                return BadRequest(new { message = "User registration failed" });
            }

            return Ok(new { message = "User registered successfully", data = result });
        }

    }
}
