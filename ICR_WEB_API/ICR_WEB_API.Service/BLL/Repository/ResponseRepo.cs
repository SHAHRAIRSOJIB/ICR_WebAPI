using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.BLL.Services;
using ICR_WEB_API.Service.Entity;
using ICR_WEB_API.Service.Model.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ICR_WEB_API.Service.BLL.Repository
{
    public class ResponseRepo : IResponseRepo
    {
        private readonly ICRSurveyDBContext _iCRSurveyDBContext;
        public ResponseRepo(ICRSurveyDBContext iCRSurveyDBContext)
        {
            _iCRSurveyDBContext = iCRSurveyDBContext;
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

        public async Task<bool> IsExist(string unifiedLicenseNumber, string ownerIDNumber)
        {
            var result = await _iCRSurveyDBContext.Responses.AsNoTracking()
                .Where(x => x.OwnerIDNumber == ownerIDNumber && x.UnifiedLicenseNumber == unifiedLicenseNumber).CountAsync();
            return result > 0;
        }

        public async Task<ResponseDTO?> GetByUnifiedLicenseNumberAndOwnerIDNumber(string unifiedLicenseNumber, string ownerIDNumber)
        {
            try
            {
                var res = new ResponseDTO();
                res = await _iCRSurveyDBContext.Responses
                    .AsNoTracking()
                    .Where(x => x.UnifiedLicenseNumber == unifiedLicenseNumber && x.OwnerIDNumber == ownerIDNumber)
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
                    IsAnswerSubmitted = resultEntry.Entity.IsAnswerSubmitted,
                    UserId = resultEntry.Entity.UserId,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseDTO?> UpdateStatus(string unifiedLicenseNumber, string ownerIDNumber, bool status = false)
        {
            try
            {
                var isExist = await IsExist(unifiedLicenseNumber, ownerIDNumber);

                if (!isExist) return null;

                var response = await _iCRSurveyDBContext.Responses.Where(x => x.UnifiedLicenseNumber == unifiedLicenseNumber && x.OwnerIDNumber == ownerIDNumber).FirstOrDefaultAsync();

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
