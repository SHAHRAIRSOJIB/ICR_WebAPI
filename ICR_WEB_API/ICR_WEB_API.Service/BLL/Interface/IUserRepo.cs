using ICR_WEB_API.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICR_WEB_API.Service.BLL.Interface
{
    public interface IUserRepo
    {
        Task<List<User>> GetAll();
        Task<int> SaveUser(User entity);
        Task<string> Update(User entity);
        Task<int> SaveAdmin(User entity);
    }
}
