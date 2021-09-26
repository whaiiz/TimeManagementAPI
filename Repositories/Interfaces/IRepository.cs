using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeManagementAPI.Models;

namespace TimeManagementAPI.Repositories.Interfaces
{
    public interface IRepository<T> where T: BaseModel
    {
        Task Create(T entity);

        Task<T> GetById(Guid id);

        Task<ICollection<T>> GetAll();
        
        Task Update(T entity);

        Task Delete(Guid id);
    }
}
