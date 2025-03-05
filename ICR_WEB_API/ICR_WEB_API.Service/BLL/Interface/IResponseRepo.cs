using ICR_WEB_API.Service.Entity;
using ICR_WEB_API.Service.Model.DTOs;

namespace ICR_WEB_API.Service.BLL.Interface
{
    public interface IResponseRepo
    {
        Task<ResponseDTO?> GetById(int id);
        Task<bool> IsExist(Response entity);
        Task<List<Response>> GetAll();
        Task<ResponseDTO?> Save(Response entity);
    }
}
