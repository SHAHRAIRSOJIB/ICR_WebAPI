using ICR_WEB_API.Service.Entity;
using ICR_WEB_API.Service.Model.DTOs;

namespace ICR_WEB_API.Service.BLL.Interface
{
    public interface IAnswerRepo
    {
        Task<List<AnswerDTO>> GetAll();
        Task<int> Save(List<Answer> entities);
        Task<string> Update(Answer entity);
    }
}
