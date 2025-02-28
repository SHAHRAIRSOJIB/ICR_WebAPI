using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.BLL.Services;
using ICR_WEB_API.Service.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICR_WEB_API.Service.BLL.Repository
{
    public class ResponseRepo: IResponseRepo
    {
        private readonly ICRSurveyDBContext _iCRSurveyDBContext;
        public ResponseRepo(ICRSurveyDBContext iCRSurveyDBContext)
        {
            _iCRSurveyDBContext = iCRSurveyDBContext;
        }
        public async Task<Response> GetById(int id)
        {
            var res = new Response();
            res = await _iCRSurveyDBContext.Responses.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);
            
            return res != null?res: new Response();
        }

        public async Task<int> Save(Response entity)
        {
            try
            {
                int res = 0;
                if (entity != null)
                {
                    var resDB = await _iCRSurveyDBContext.Responses.AddAsync(entity);
                    res = await _iCRSurveyDBContext.SaveChangesAsync();
                    return res > 0 ? res = entity.Id: 0;
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
