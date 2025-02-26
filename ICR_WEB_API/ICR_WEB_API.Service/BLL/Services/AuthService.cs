using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICR_WEB_API.Service.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ICR_WEB_API.Service.BLL.Interface;

namespace ICR_WEB_API.Service.BLL.Services
{
    public class AuthService:IAuthService
    {

        private readonly ICRSurveyDBContext _context;
        private IConfiguration _configuration;
        public AuthService(ICRSurveyDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }
        public async Task<LoginResponse> AuthenticateUser(string userName, string passWord)
        {
            LoginResponse user = new LoginResponse();
            var userData = await _context.Users.FirstOrDefaultAsync(x => x.Email == userName && x.Password == passWord);
            if (userData != null)
            {

                user.Id = userData.Id;
                user.Email = userData.Email;
                user.Name = userData.Name;
                user.Email = userData.Email;
                user.UserType = userData.UserType;
                string token = GenerateJSONWebToken(user);
                if (token != null)
                {
                    user.Token = token;
                }

            }
            return user;
        }
        private string GenerateJSONWebToken(LoginResponse userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Name, userInfo.Name),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
