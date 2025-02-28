using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.BLL.Services;
using ICR_WEB_API.Service.Entity;
using Microsoft.EntityFrameworkCore;

namespace ICR_WEB_API.Service.BLL.Repository
{
    public class QuestionsRepo : IQuestionRepo
    {
        private readonly ICRSurveyDBContext _surveyDBContext;
        public QuestionsRepo(ICRSurveyDBContext context)
        {
            _surveyDBContext = context;
        }
        public async Task<List<Question>> GetAll()
        {
            try
            {
                var list = new List<Question>();
                list = await _surveyDBContext.Questions.Include(x => x.Options).Include(x => x.RatingScaleItems).ToListAsync();
                return list;
            }
            catch (Exception ex) {
                throw ex;
            }

          
        }

        public async Task<int> Save(Question entity)
        {
            try
            {
                int res = 0;
                if (entity != null)
                {
                    await _surveyDBContext.Questions.AddAsync(entity);
                    await _surveyDBContext.SaveChangesAsync();
                    res = entity.Id;
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<string> Update(Question entity)
        {
            try
            {
                string response = "";
                var exist = await _surveyDBContext.Questions.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (exist != null)
                {
                    _surveyDBContext.Questions.Update(entity);
                    await _surveyDBContext.SaveChangesAsync();
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
