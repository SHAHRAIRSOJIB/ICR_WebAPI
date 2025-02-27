using ICR_WEB_API.Service.Entity;

namespace ICR_WEB_API.Service.BLL.Interface
{
    public interface IQuestionRepo
    {
        Task<List<Question>> GetAll();
        Task<int> Save(Question entity);
        Task<string> Update(Question entity);
    }
}
