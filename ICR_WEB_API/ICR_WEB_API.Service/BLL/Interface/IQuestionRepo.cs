using ICR_WEB_API.Service.Entity;
using ICR_WEB_API.Service.Model.DTOs;

namespace ICR_WEB_API.Service.BLL.Interface
{
    public interface IQuestionRepo
    {
        Task<List<QuestionDTO>> GetAll();
        Task<int> Save(Question entity);
        Task<string> Update(Question entity);
    }
}
