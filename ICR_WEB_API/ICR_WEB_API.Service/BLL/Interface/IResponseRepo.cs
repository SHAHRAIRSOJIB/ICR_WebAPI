using ICR_WEB_API.Service.Entity;
using ICR_WEB_API.Service.Model.DTOs;

namespace ICR_WEB_API.Service.BLL.Interface
{
    public interface IResponseRepo
    {
        Task<List<ResponseWithQuestionsAndAnswerDTO>> GetAllFormatedResponse();
        Task<ResponseDTO?> GetById(int id);
        Task<bool> IsExist(string unifiedLicenseNumber);
        Task<ResponseDTO?> GetByUnifiedLicenseNumber(string unifiedLicenseNumber);
        Task<ResponseDTO?> UpdateStatus(string unifiedLicenseNumber, bool status = false);
        Task<List<Response>> GetAll();
        Task<ResponseDTO?> Save(Response entity);
    }
}
