using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.BLL.Services;
using ICR_WEB_API.Service.Entity;
using ICR_WEB_API.Service.Enum;
using ICR_WEB_API.Service.Model;
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

        public async Task<int> SaveUser(User entity)
        {
            try
            {
                int res = 0;
                if (entity != null)
                {
                    entity.UserType = EnumCollection.UserType.User;
                    await _icrSurveySurveyDBContext.Users.AddAsync(entity);
                    await _icrSurveySurveyDBContext.SaveChangesAsync();
                    res = entity.Id;
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<string> Update(User entity)
        {
            try
            {
                string response = "";
                var exist = await _icrSurveySurveyDBContext.Users.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (exist != null)
                {
                    _icrSurveySurveyDBContext.Users.Update(entity);
                    await _icrSurveySurveyDBContext.SaveChangesAsync();
                    response = "Data Updated Successfully";

                }
                else
                {
                    response = "No Data Found.";
                }
                return response;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
                    entity.UserType = EnumCollection.UserType.Admin;
                    await _icrSurveySurveyDBContext.Users.AddAsync(entity);
                    await _icrSurveySurveyDBContext.SaveChangesAsync();
                    res = entity.Id;
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<LoginResponse> VerifyUser(string userName, string password)
        {
            try
            {
                LoginResponse LoginResponse = new LoginResponse();
                if (!userName.IsNullOrEmpty() && !password.IsNullOrEmpty())
                {
                    var user = await _icrSurveySurveyDBContext.Users.FirstOrDefaultAsync(x =>
                        x.Email == userName && x.Password == password);
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
    }
}
