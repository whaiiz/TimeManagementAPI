using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeManagementAPI.Exceptions;
using TimeManagementAPI.Models;
using TimeManagementAPI.Repositories.Interfaces;

namespace TimeManagementAPI.Repositories.MongoDb
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        protected readonly IMongoCollection<T> Collection;

        public Repository(IMongoCollection<T> collection)
        {
            Collection = collection;
        }

        public async Task Create(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            await Collection.InsertOneAsync(entity);
        }

        public async Task<ICollection<T>> GetAll()
        {
            return await Collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<T> GetById(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            var result = await Collection.Find(filter).FirstOrDefaultAsync();

            if (result == null) throw new EntityNotFoundException();

            return result;
        }

        public async Task Update(T entity)
        {
            var e = await GetById(entity.Id);
            entity.CreatedAt = e.CreatedAt;
            entity.UpdatedAt = DateTime.Now;
            await Collection.ReplaceOneAsync(e => e.Id.Equals(entity.Id), entity);
        }

        public async Task Delete(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);

            await GetById(id);
            await Collection.DeleteOneAsync(filter);
        }
    }
}
