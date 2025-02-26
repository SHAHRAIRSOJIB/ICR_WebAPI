using ICR_WEB_API.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICR_WEB_API.Service.BLL.Interface
{
    public interface IAuthService
    {
        Task<LoginResponse> AuthenticateUser(string userName, string passWord);
    }
}
