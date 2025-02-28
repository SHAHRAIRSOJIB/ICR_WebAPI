using ICR_WEB_API.Service.Entity;

namespace ICR_WEB_API.Service.BLL.Interface
{
    public interface IResponseRepo
    {
        Task<Response> GetById(int id);
        Task<int> Save(Response entity);

    }
}
