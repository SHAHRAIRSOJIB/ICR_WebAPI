using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.BLL.Services;
using ICR_WEB_API.Service.Entity;
using ICR_WEB_API.Service.Enum;
using ICR_WEB_API.Service.Model;
using ICR_WEB_API.Service.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ICR_WEB_API.Service.BLL.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly ICRSurveyDBContext _icrSurveySurveyDBContext;

        public UserRepo(ICRSurveyDBContext icrSurveyDbContext)
        {
            _icrSurveySurveyDBContext = icrSurveyDbContext;
        }

        public async Task<List<User>> GetAll()
        {
            var list = new List<User>();
            list = await _icrSurveySurveyDBContext.Users.ToListAsync();
            return list;
        }

        public async Task<User?> SaveUser(UserDTO entity)
        {
            try
            {
                if (entity == null) return null;

                entity.UserType = UserType.User;

                var user = new User()
                {
                    Name = entity.Name,
                    Email = entity.Email,
                    Password = entity.Password,
                    UserType = entity.UserType
                };

                var resultEntry = await _icrSurveySurveyDBContext.Users.AddAsync(user);
                await _icrSurveySurveyDBContext.SaveChangesAsync();

                if (resultEntry.Entity == null || resultEntry.Entity.Id <= 0) return null;

                return resultEntry.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User?> Update(UserUpdateDTO entity)
        {
            try
            {
                if (entity == null) return null;

                var user = await _icrSurveySurveyDBContext.Users.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (user == null) return null;

                user.Name = entity.Name;
                user.Email = entity.Email;

                var resultEntry = _icrSurveySurveyDBContext.Users.Update(user);
                await _icrSurveySurveyDBContext.SaveChangesAsync();

                if (resultEntry.Entity == null || resultEntry.Entity.Id <= 0) return null;

                return resultEntry.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> SaveAdmin(User entity)
        {
            try
            {
                int res = 0;
                if (entity != null)
                {
                    entity.UserType = UserType.Admin;
                    await _icrSurveySurveyDBContext.Users.AddAsync(entity);
                    await _icrSurveySurveyDBContext.SaveChangesAsync();
                    res = entity.Id;
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<LoginResponse> VerifyUser(string userName, string password)
        {
            try
            {
                LoginResponse LoginResponse = new LoginResponse();
                if (!userName.IsNullOrEmpty() && !password.IsNullOrEmpty())
                {
                    var user = await _icrSurveySurveyDBContext.Users.FirstOrDefaultAsync(x => x.Email == userName && x.Password == password);
                    if (user != null)
                    {
                        LoginResponse.Email = user.Email;
                        LoginResponse.Id = user.Id;
                        LoginResponse.Name = user.Name;
                    }
                    else
                    {
                        LoginResponse = null;
                    }
                }
                return LoginResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<User?> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            try
            {
                if (resetPasswordDTO == null) return null;

                var user = await _icrSurveySurveyDBContext.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Email == resetPasswordDTO.Email && x.Password == resetPasswordDTO.CurrentPassword);

                if (user == null)
                {
                    return null;
                }

                user.Password = resetPasswordDTO.NewPassword;
                var resultEntry = _icrSurveySurveyDBContext.Users.Update(user);
                await _icrSurveySurveyDBContext.SaveChangesAsync();

                if (resultEntry.Entity == null || resultEntry.Entity.Id <= 0) return null;

                return resultEntry.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsExistById(int id)
        {
            var result = await _icrSurveySurveyDBContext.Users.AsNoTracking().Where(x => x.Id == id).CountAsync();

            return result > 0;
        }

        public async Task<bool> IsExistByEmail(string email)
        {
            var result = await _icrSurveySurveyDBContext.Users.AsNoTracking().Where(x => x.Email == email).CountAsync();

            return result > 0;
        }

        public async Task<User?> GetById(int id)
        {
            return await _icrSurveySurveyDBContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _icrSurveySurveyDBContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
