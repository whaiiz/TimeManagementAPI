using MongoDB.Driver;
using System;
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

        public async Task UpdateDate(string id, DateTime date)
        {
            var entity = await GetById(id);
            entity.UpdatedAt = DateTime.Now;
            entity.DateTime = date;
            await Collection.ReplaceOneAsync(e => e.Id.Equals(id), entity);
        }

        public async Task UpdateStatus(string id, string status)
        {
            var entity = await GetById(id);
            entity.UpdatedAt = DateTime.Now;
            entity.Status = status;
            await Collection.ReplaceOneAsync(e => e.Id.Equals(id), entity);
        }
    }
}
