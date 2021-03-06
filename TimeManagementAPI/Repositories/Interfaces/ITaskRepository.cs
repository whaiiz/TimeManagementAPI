using System.Collections.Generic;
using System.Threading.Tasks;
using TimeManagementAPI.Models;

namespace TimeManagementAPI.Repositories.Interfaces
{
    public interface ITaskRepository : IRepository<TaskModel>
    {
        Task<ICollection<TaskModel>> GetByUser(string username);
    }
}