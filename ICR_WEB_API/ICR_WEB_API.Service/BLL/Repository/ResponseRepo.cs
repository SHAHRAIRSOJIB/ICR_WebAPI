using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.BLL.Services;
using ICR_WEB_API.Service.Entity;
using ICR_WEB_API.Service.Enum;
using ICR_WEB_API.Service.Model.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace ICR_WEB_API.Service.BLL.Repository
{
    public class ResponseRepo : IResponseRepo
    {
        private readonly ICRSurveyDBContext _iCRSurveyDBContext;
        private readonly IWebHostEnvironment _env;
        public ResponseRepo(ICRSurveyDBContext iCRSurveyDBContext, IWebHostEnvironment env)
        {
            _iCRSurveyDBContext = iCRSurveyDBContext;
            _env = env;
        }

        public async Task<string?> UploadImage(UploadFileDTO fileInfo)
        {
            if (fileInfo == null)
            {
                return "Invalid data";
            }

            var base64ImageString = fileInfo.Base64ImageString;
            var unifiedLicenseNumber = fileInfo.UnifiedLicenseNumber;

            var dataParts = base64ImageString.Split(',');
            if (dataParts.Length == 2)
            {
                base64ImageString = dataParts[1];
            }

            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64ImageString);

                if (_env.WebRootPath == null)
                {
                    return "wwwroot folder is not created";
                }

                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var extension = ".jpg";
                var uniqueFileName = $"{unifiedLicenseNumber}{extension}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var ms = new MemoryStream(imageBytes))
                {
                    using Image image = await Image.LoadAsync(ms);
                    int maxWidth = 1024;
                    if (image.Width > maxWidth)
                    {
                        image.Mutate(x => x.Resize(maxWidth, 0)); // 0 height for maintains the aspect ratio.
                    }

                    var encoder = new JpegEncoder { Quality = 75 };
                    await image.SaveAsync(filePath, encoder);
                }

                var fileUrl = $"/uploads/{uniqueFileName}";

                return fileUrl;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<FormattedResponseDto> GetAllFormatedResponse()
        {
            var questions = await _iCRSurveyDBContext.Questions
                .Where(q => q.IsShowable)
                .Include(q => q.Options)
                .Include(q => q.RatingScaleItems)
                .OrderBy(q => q.SortOrder)
                .ToListAsync();

            var columns = new List<ColumnDefinition>();
            foreach (var question in questions)
            {
                switch (question.Type)
                {
                    case QuestionType.Text:
                        var uniqueKey = $"{question.Id}-{question.Text}";
                        var displayLabel = $"{question.Text}";
                        columns.Add(new ColumnDefinition
                        {
                            QuestionId = question.Id,
                            UniqueKey = uniqueKey,
                            DisplayLabel = displayLabel
                        });
                        break;

                    case QuestionType.Select:
                    case QuestionType.Checkbox:
                        if (question.Options != null)
                        {
                            foreach (var option in question.Options)
                            {
                                var uniqueKey1 = $"{question.Id}-{question.Text}-{option.Id}-{option.OptionText}";
                                var displayLabel1 = $"{question.Text} - {option.OptionText}";
                                columns.Add(new ColumnDefinition
                                {
                                    QuestionId = question.Id,
                                    OptionId = option.Id,
                                    UniqueKey = uniqueKey1,
                                    DisplayLabel = displayLabel1
                                });
                            }
                        }
                        break;

                    case QuestionType.Rating:
                        if (question.RatingScaleItems != null)
                        {
                            foreach (var ratingScaleItem in question.RatingScaleItems)
                            {
                                var uniqueKey1 = $"{question.Id}-{question.Text}-{ratingScaleItem.Id}-{ratingScaleItem.ItemText}";
                                var displayLabel1 = $"{question.Text} - {ratingScaleItem.ItemText}";
                                columns.Add(new ColumnDefinition
                                {
                                    QuestionId = question.Id,
                                    RatingItemId = ratingScaleItem.Id,
                                    UniqueKey = uniqueKey1,
                                    DisplayLabel = displayLabel1
                                });
                            }
                        }
                        break;
                }
            }

            var responses = await _iCRSurveyDBContext.Responses
                .Include(r => r.Answers)
                .ThenInclude(a => a.SelectedOption)
                .Include(r => r.Answers)
                .ThenInclude(a => a.RatingItem)
                .Include(r => r.User)
                .ToListAsync();

            var formattedResponse = new FormattedResponseDto
            {
                Columns = [.. columns],
                Rows = new List<FormattedResponseRowDto>()
            };

            foreach (var response in responses)
            {
                var answerDict = columns.GroupBy(c => c.UniqueKey).ToDictionary(g => g.Key, g => string.Empty);

                foreach (var answer in response.Answers)
                {
                    var question = questions.FirstOrDefault(q => q.Id == answer.QuestionId);
                    if (question != null)
                    {
                        if (question.Type == QuestionType.Text)
                        {
                            answerDict[$"{question.Id}-{question.Text}"] = answer.TextResponse;
                        }
                        else if (question.Type == QuestionType.Select)
                        {
                            if (answer.SelectedOptionId.HasValue)
                            {
                                var option = question.Options.FirstOrDefault(o => o.Id == answer.SelectedOptionId.Value);
                                if (option != null)
                                {
                                    var key = $"{question.Id}-{question.Text}-{option.Id}-{option.OptionText}";
                                    answerDict[key] = option.OptionText;
                                }
                            }
                        }
                        else if (question.Type == QuestionType.Checkbox)
                        {
                            if (answer.SelectedOptionId.HasValue)
                            {
                                var option = question.Options.FirstOrDefault(o => o.Id == answer.SelectedOptionId.Value);
                                if (option != null)
                                {
                                    var key = $"{question.Id}-{question.Text}-{option.Id}-{option.OptionText}";
                                    // For checkboxes, there may be multiple entries – each gets its own column value.
                                    answerDict[key] = option.OptionText;
                                }
                            }
                        }
                        else if (question.Type == QuestionType.Rating)
                        {
                            if (answer.RatingItemId.HasValue)
                            {
                                var ratingScaleItem = question.RatingScaleItems.FirstOrDefault(i => i.Id == answer.RatingItemId.Value);
                                if (ratingScaleItem != null)
                                {
                                    var key = $"{question.Id}-{question.Text}-{ratingScaleItem.Id}-{ratingScaleItem.ItemText}";
                                    answerDict[key] = answer.RatingValue;
                                }
                            }
                        }
                    }
                }

                formattedResponse.Rows.Add(new FormattedResponseRowDto
                {
                    ResponseId = response.Id,
                    SubmissionDate = response.SubmissionDate,
                    UnifiedLicenseNumber = response.UnifiedLicenseNumber,
                    LicenseIssueDateLabel = response.LicenseIssueDateLabel,
                    ShopName = response.ShopName,
                    DistrictName = response.DistrictName,
                    StreetName = response.StreetName,
                    Municipality = response.Municipality,
                    FullAddress = response.FullAddress,
                    AIESECActivity = response.AIESECActivity,
                    OwnerIDNumber = response.OwnerIDNumber,
                    OwnerName = response.OwnerName,
                    ImageLicensePlate = response.ImageLicensePlate,
                    IsAnswerSubmitted = response.IsAnswerSubmitted,
                    User = response.User,
                    Answers = answerDict
                });
            }

            return formattedResponse;
        }

        public async Task<List<Response>> GetAll()
        {
            try
            {
                var list = await _iCRSurveyDBContext.Responses
                    .Include(x => x.Answers)
                    .Include(x => x.User)
                    .ToListAsync();
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseDTO?> GetById(int id)
        {
            try
            {
                var res = new ResponseDTO();
                res = await _iCRSurveyDBContext.Responses
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .Select(x => new ResponseDTO
                    {
                        Id = x.Id,
                        SubmissionDate = x.SubmissionDate,
                        ShopName = x.ShopName,
                        OwnerName = x.OwnerName,
                        DistrictName = x.DistrictName,
                        StreetName = x.StreetName,
                        UnifiedLicenseNumber = x.UnifiedLicenseNumber,
                        LicenseIssueDateLabel = x.LicenseIssueDateLabel,
                        OwnerIDNumber = x.OwnerIDNumber,
                        AIESECActivity = x.AIESECActivity,
                        Municipality = x.Municipality,
                        FullAddress = x.FullAddress,
                        ImageLicensePlate = x.ImageLicensePlate,
                        IsAnswerSubmitted = x.IsAnswerSubmitted,
                        UserId = x.UserId
                    }).SingleOrDefaultAsync();

                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsExist(string unifiedLicenseNumber)
        {
            var result = await _iCRSurveyDBContext.Responses.AsNoTracking()
                .Where(x => x.UnifiedLicenseNumber == unifiedLicenseNumber).CountAsync();
            return result > 0;
        }

        public async Task<ResponseDTO?> GetByUnifiedLicenseNumber(string unifiedLicenseNumber)
        {
            try
            {
                var res = new ResponseDTO();
                res = await _iCRSurveyDBContext.Responses
                    .AsNoTracking()
                    .Where(x => x.UnifiedLicenseNumber == unifiedLicenseNumber)
                    .Select(x => new ResponseDTO
                    {
                        Id = x.Id,
                        SubmissionDate = x.SubmissionDate,
                        ShopName = x.ShopName,
                        OwnerName = x.OwnerName,
                        DistrictName = x.DistrictName,
                        StreetName = x.StreetName,
                        UnifiedLicenseNumber = x.UnifiedLicenseNumber,
                        LicenseIssueDateLabel = x.LicenseIssueDateLabel,
                        OwnerIDNumber = x.OwnerIDNumber,
                        AIESECActivity = x.AIESECActivity,
                        Municipality = x.Municipality,
                        FullAddress = x.FullAddress,
                        IsAnswerSubmitted = x.IsAnswerSubmitted,
                        UserId = x.UserId
                    }).SingleOrDefaultAsync();

                return res ?? null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseDTO?> Save(Response entity)
        {
            try
            {
                if (entity == null) return null;

                var fileUrl = await UploadImage(new UploadFileDTO()
                {
                    Base64ImageString = entity.ImageLicensePlate,
                    UnifiedLicenseNumber = entity.UnifiedLicenseNumber
                });

                entity.ImageLicensePlate = fileUrl ?? "";

                var resultEntry = await _iCRSurveyDBContext.Responses.AddAsync(entity);
                await _iCRSurveyDBContext.SaveChangesAsync();

                if (resultEntry.Entity == null || resultEntry.Entity.Id <= 0) return null;

                return new ResponseDTO()
                {
                    Id = resultEntry.Entity.Id,
                    SubmissionDate = resultEntry.Entity.SubmissionDate,
                    ShopName = resultEntry.Entity.ShopName,
                    OwnerName = resultEntry.Entity.OwnerName,
                    DistrictName = resultEntry.Entity.DistrictName,
                    StreetName = resultEntry.Entity.StreetName,
                    UnifiedLicenseNumber = resultEntry.Entity.UnifiedLicenseNumber,
                    LicenseIssueDateLabel = resultEntry.Entity.LicenseIssueDateLabel,
                    OwnerIDNumber = resultEntry.Entity.OwnerIDNumber,
                    AIESECActivity = resultEntry.Entity.AIESECActivity,
                    Municipality = resultEntry.Entity.Municipality,
                    FullAddress = resultEntry.Entity.FullAddress,
                    ImageLicensePlate = resultEntry.Entity.ImageLicensePlate,
                    IsAnswerSubmitted = resultEntry.Entity.IsAnswerSubmitted,
                    UserId = resultEntry.Entity.UserId,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseDTO?> UpdateStatus(string unifiedLicenseNumber, bool status = false)
        {
            try
            {
                var isExist = await IsExist(unifiedLicenseNumber);

                if (!isExist) return null;

                var response = await _iCRSurveyDBContext.Responses.Where(x => x.UnifiedLicenseNumber == unifiedLicenseNumber).FirstOrDefaultAsync();

                if (response == null) return null;

                response.IsAnswerSubmitted = status;

                var resultEntry = _iCRSurveyDBContext.Responses.Update(response);
                await _iCRSurveyDBContext.SaveChangesAsync();

                if (resultEntry.Entity == null || resultEntry.Entity.Id <= 0) return null;

                return new ResponseDTO()
                {
                    Id = resultEntry.Entity.Id,
                    SubmissionDate = resultEntry.Entity.SubmissionDate,
                    ShopName = resultEntry.Entity.ShopName,
                    OwnerName = resultEntry.Entity.OwnerName,
                    DistrictName = resultEntry.Entity.DistrictName,
                    StreetName = resultEntry.Entity.StreetName,
                    UnifiedLicenseNumber = resultEntry.Entity.UnifiedLicenseNumber,
                    LicenseIssueDateLabel = resultEntry.Entity.LicenseIssueDateLabel,
                    OwnerIDNumber = resultEntry.Entity.OwnerIDNumber,
                    AIESECActivity = resultEntry.Entity.AIESECActivity,
                    Municipality = resultEntry.Entity.Municipality,
                    FullAddress = resultEntry.Entity.FullAddress,
                    IsAnswerSubmitted = resultEntry.Entity.IsAnswerSubmitted,
                    UserId = resultEntry.Entity.UserId,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
