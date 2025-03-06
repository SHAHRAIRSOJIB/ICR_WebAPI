using ICR_WEB_API.Service.Entity;
using ICR_WEB_API.Service.Model.DTOs;

namespace ICR_WEB_API.Service.BLL.Interface
{
    public interface IResponseRepo
    {
        Task<ResponseDTO?> GetById(int id);
        Task<bool> IsExist(string unifiedLicenseNumber, string ownerIDNumber);
        Task<ResponseDTO?> GetByUnifiedLicenseNumberAndOwnerIDNumber(string unifiedLicenseNumber, string ownerIDNumber);
        Task<ResponseDTO?> UpdateStatus(string unifiedLicenseNumber, string ownerIDNumber, bool status = false);
        Task<List<Response>> GetAll();
        Task<ResponseDTO?> Save(Response entity);
    }
}
