using ICR_WEB_API.Service.Entity;
using ICR_WEB_API.Service.Model;
using ICR_WEB_API.Service.Model.DTOs;

namespace ICR_WEB_API.Service.BLL.Interface
{
    public interface IUserRepo
    {
        Task<List<User>> GetAll();
        Task<User?> GetById(int id);
        Task<User?> GetByEmail(string email);
        Task<User?> SaveUser(UserDTO entity);
        Task<User?> Update(UserUpdateDTO entity);
        Task<bool> IsExistById(int id);
        Task<bool> IsExistByEmail(string email);
        Task<int> SaveAdmin(User entity);
        Task<LoginResponse> VerifyUser(string userName, string password);
        Task<User?> ResetPassword(ResetPasswordDTO resetPasswordDTO);
    }
}
