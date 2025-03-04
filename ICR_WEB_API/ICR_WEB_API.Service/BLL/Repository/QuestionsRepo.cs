using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.BLL.Services;
using ICR_WEB_API.Service.Entity;
using ICR_WEB_API.Service.Model.DTOs;
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

        public async Task<QuestionDTO?> GetAsync(int id)
        {
            var question = await _surveyDBContext.Questions
                    .Include(x => x.Options)
                    .Include(x => x.RatingScaleItems)
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (question == null)
                return null;

            var questionDTO = new QuestionDTO
            {
                Id = question.Id,
                Text = question.Text,
                Type = question.Type,
                SortOrder = question.SortOrder,
                IsShowable = question.IsShowable,
                Options = question.Options?.Select(o => new OptionDTO
                {
                    Id = o.Id,
                    OptionText = o.OptionText
                }).ToList(),
                RatingScaleItems = question.RatingScaleItems?.Select(r => new RatingScaleItemDTO
                {
                    Id = r.Id,
                    ItemText = r.ItemText
                }).ToList()
            };

            return questionDTO;
        }

        public async Task<List<QuestionDTO>> GetAll()
        {
            try
            {
                var list = await _surveyDBContext.Questions
                    .Include(x => x.Options)
                    .Include(x => x.RatingScaleItems)
                    .ToListAsync();

                var questionDTOs = list.Select(q => new QuestionDTO
                {
                    Id = q.Id,
                    Text = q.Text,
                    Type = q.Type,
                    SortOrder = q.SortOrder,
                    IsShowable = q.IsShowable,
                    Options = q.Options?.Select(o => new OptionDTO
                    {
                        Id = o.Id,
                        OptionText = o.OptionText
                    }).ToList(),
                    RatingScaleItems = q.RatingScaleItems?.Select(r => new RatingScaleItemDTO
                    {
                        Id = r.Id,
                        ItemText = r.ItemText
                    }).ToList()
                }).ToList();

                return questionDTOs;
            }
            catch (Exception ex)
            {
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
