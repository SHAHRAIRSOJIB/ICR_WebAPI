using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.BLL.Services;
using ICR_WEB_API.Service.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICR_WEB_API.Service.BLL.Repository
{
    public  class QuestionsRepo: IQuestionRepo
    {
        private readonly ICRSurveyDBContext _surveyDBContext;
        public QuestionsRepo(ICRSurveyDBContext context)
        {
                _surveyDBContext = context;
        }
        public async Task<List<Question>> GetAll()
        {
            var list = new List<Question>();
            list = await _surveyDBContext.Questions.ToListAsync();
            return list;
        }
    }
}
