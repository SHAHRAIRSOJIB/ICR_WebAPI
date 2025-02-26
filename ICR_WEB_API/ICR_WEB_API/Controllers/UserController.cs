using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.Entity;
using Microsoft.AspNetCore.Http;
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
        public async Task<string> Update(User entity)
        {
            return await _userRepo.Update(entity);
        }
        [HttpPost("saveAdmin")]
        public async Task<int> SaveAdmin(User entity)
        {
            return await _userRepo.SaveAdmin(entity);
        }
    }
}
