using System;
using System.Threading.Tasks;
using TimeManagementAPI.Models;

namespace TimeManagementAPI.Repositories.Interfaces
{
    public interface ITaskRepository : IRepository<TaskModel>
    {
        Task UpdateDate(string id, DateTime date);
        Task UpdateStatus(string id, string status);
    }
}