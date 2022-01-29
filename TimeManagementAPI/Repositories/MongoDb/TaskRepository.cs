using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<ICollection<TaskModel>> GetByUser(string username)
        {
            var filter = Builders<TaskModel>.Filter.Eq("username", username);
            var result = await Collection.Find(filter).ToListAsync();

            return result;
        }
    }
}
