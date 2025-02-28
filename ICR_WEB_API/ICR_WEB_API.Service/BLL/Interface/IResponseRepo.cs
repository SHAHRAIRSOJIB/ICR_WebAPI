using ICR_WEB_API.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICR_WEB_API.Service.BLL.Interface
{
    public interface IResponseRepo
    {
        Task<Response> GetById(int id);
        Task<int> Save(Response entity);

    }
}
