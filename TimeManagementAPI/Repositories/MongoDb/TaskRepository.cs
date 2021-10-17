using MongoDB.Driver;
using TimeManagementAPI.Models;
using TimeManagementAPI.Repositories.Interfaces;
using TimeManagementAPI.Repositories.MongoDb;

namespace TimeManagementAPI.Repositories
{
    public class TaskRepository : Repository<TaskModel>, ITaskRepository
    {
        public TaskRepository(IMongoCollection<TaskModel> collection) : base (collection)
        {

        }
    }
}
