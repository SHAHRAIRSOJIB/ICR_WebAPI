using ICR_WEB_API.Service.Model;

namespace ICR_WEB_API.Service.BLL.Interface
{
    public interface IAuthService
    {
        Task<LoginResponse> AuthenticateUser(string userName, string passWord);
    }
}
