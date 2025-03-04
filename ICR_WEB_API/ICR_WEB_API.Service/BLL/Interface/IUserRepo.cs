using ICR_WEB_API.Service.Entity;
using ICR_WEB_API.Service.Model;

namespace ICR_WEB_API.Service.BLL.Interface
{
    public interface IUserRepo
    {
        Task<List<User>> GetAll();
        Task<int> SaveUser(User entity);
        Task<User?> Update(User entity);
        Task<bool> IsExist(User entity);
        Task<int> SaveAdmin(User entity);
        Task<LoginResponse> VerifyUser(string userName, string password);
        Task<string> ForgetPassword(ForgetPasswordDTO resetPasswordDTO);
    }
}
