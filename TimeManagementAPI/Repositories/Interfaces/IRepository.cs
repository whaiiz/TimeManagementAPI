using System.Collections.Generic;
using System.Threading.Tasks;
using TimeManagementAPI.Models;

namespace TimeManagementAPI.Repositories.Interfaces
{
    public interface IRepository<T> where T: BaseModel
    {
        Task<T> Create(T entity);

        Task<T> GetById(string id);

        Task<ICollection<T>> GetAll();
        
        Task Update(T entity);

        Task Delete(string id);
    }
}
