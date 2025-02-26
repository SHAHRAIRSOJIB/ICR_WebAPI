﻿using ICR_WEB_API.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICR_WEB_API.Service.BLL.Interface
{
    public interface IAnswerRepo 
    {
        Task<List<Answer>> GetAll();
        Task<int> Save(Answer entity);
        Task<string> Update(Answer entity);
    }
}
