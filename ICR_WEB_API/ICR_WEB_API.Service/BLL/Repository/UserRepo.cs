using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.BLL.Services;
using ICR_WEB_API.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ICR_WEB_API.Service.Enum;

namespace ICR_WEB_API.Service.BLL.Repository
{
    public  class UserRepo : IUserRepo
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
    }
}
