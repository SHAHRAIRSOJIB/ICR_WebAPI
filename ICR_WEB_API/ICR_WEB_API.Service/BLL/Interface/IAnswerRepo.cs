﻿using ICR_WEB_API.Service.Entity;

namespace ICR_WEB_API.Service.BLL.Interface
{
    public interface IAnswerRepo
    {
        Task<List<Answer>> GetAll();
        Task<int> Save(Answer entity);
        Task<string> Update(Answer entity);
    }
}
