using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.BLL.Services;
using ICR_WEB_API.Service.Entity;
using ICR_WEB_API.Service.Model.DTOs;
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
        public async Task<List<AnswerDTO>> GetAll()
        {
            var list = await _icrSurveySurveyDBContext.Answers
                .Include(x => x.Question)
                    .ThenInclude(q => q.Options)
                .Include(x => x.Question)
                    .ThenInclude(q => q.RatingScaleItems)
                .Include(x => x.Response)
                .ToListAsync();

            var answerDTOs = list.Select(a => new AnswerDTO
            {
                Id = a.Id,
                ResponseId = a.ResponseId,
                QuestionId = a.QuestionId,
                SelectedOptionId = a.SelectedOptionId,
                RatingItemId = a.RatingItemId,
                RatingValue = a.RatingValue,
                TextResponse = a.TextResponse,

                // Map Question to QuestionDTO
                Question = a.Question != null ? new QuestionDTO
                {
                    Id = a.Question.Id,
                    Text = a.Question.Text,
                    Type = a.Question.Type,
                    Options = a.Question.Options?.Select(o => new OptionDTO
                    {
                        Id = o.Id,
                        OptionText = o.OptionText
                    }).ToList(),
                    RatingScaleItems = a.Question.RatingScaleItems?.Select(r => new RatingScaleItemDTO
                    {
                        Id = r.Id,
                        ItemText = r.ItemText
                    }).ToList()
                } : null,

                // Map Response to ResponseDTO
                Response = a.Response != null ? new ResponseDTO
                {
                    Id = a.Response.Id,
                    SubmissionDate = a.Response.SubmissionDate,
                    ShopName = a.Response.ShopName,
                    OwnerName = a.Response.OwnerName,
                    UnifiedLicenseNumber = a.Response.UnifiedLicenseNumber,
                    LicenseIssueDateLabel = a.Response.LicenseIssueDateLabel,
                    OwnerIDNumber = a.Response.OwnerIDNumber,
                    AIESECActivity = a.Response.AIESECActivity,
                    Municipality = a.Response.Municipality,
                    FullAddress = a.Response.FullAddress,
                    UserId = a.Response.UserId
                } : null

            }).ToList();

            return answerDTOs;
        }


        public async Task<int> Save(List<Answer> entities)
        {
            try
            {
                int res = 0;
                if (entities != null && entities.Count > 0)
                {
                    await _icrSurveySurveyDBContext.Answers.AddRangeAsync(entities);

                    var r = await _icrSurveySurveyDBContext.Responses.FirstOrDefaultAsync(x => x.Id == entities.First().ResponseId);

                    if (r == null) return 0;

                    r.IsSubmited = true;
                    _icrSurveySurveyDBContext.Responses.Update(r);

                    var resDB = await _icrSurveySurveyDBContext.SaveChangesAsync();
                    return resDB > 0 ? res = resDB : 0;
                }
                return res;
            }
            catch (Exception)
            {
                throw;
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
