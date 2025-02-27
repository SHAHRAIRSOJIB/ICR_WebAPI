﻿using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.BLL.Services;
using ICR_WEB_API.Service.Entity;
using Microsoft.EntityFrameworkCore;

namespace ICR_WEB_API.Service.BLL.Repository
{
    public class AnswerRepo : IAnswerRepo
    {
        private readonly ICRSurveyDBContext _icrSurveySurveyDBContext;

        public AnswerRepo(ICRSurveyDBContext icrSurveyDbContext)
        {
            _icrSurveySurveyDBContext = icrSurveyDbContext;
        }
        public async Task<List<Answer>> GetAll()
        {
            var list = new List<Answer>();
            list = await _icrSurveySurveyDBContext.Answers.ToListAsync();
            return list;
        }

        public async Task<int> Save(List<Answer> entities)
        {
            try
            {
                int res = 0;
                if (entities != null)
                {
                    await _icrSurveySurveyDBContext.Answers.AddRangeAsync(entities);
                    var resDB = await _icrSurveySurveyDBContext.SaveChangesAsync();
                    return resDB > 0? res=resDB : 0;
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<string> Update(Answer entity)
        {
            try
            {
                string response = "";
                var exist = await _icrSurveySurveyDBContext.Answers.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (exist != null)
                {
                    _icrSurveySurveyDBContext.Answers.Update(entity);
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
    }

}
