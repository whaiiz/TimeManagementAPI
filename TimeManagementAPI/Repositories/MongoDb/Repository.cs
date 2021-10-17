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
        private readonly IMongoCollection<T> _collection;

        public Repository(IMongoCollection<T> collection)
        {
            _collection = collection;
        }

        public async Task Create(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            await _collection.InsertOneAsync(entity);
        }

        public async Task<ICollection<T>> GetAll()
        {
            return await _collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<T> GetById(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            var result = await _collection.Find(filter).FirstOrDefaultAsync();

            if (result == null) throw new EntityNotFoundException();

            return result;
        }

        public async Task Update(T entity)
        {
            await GetById(entity.Id);
            await _collection.ReplaceOneAsync(new BsonDocument("Id", entity.Id), entity);
        }

        public async Task Delete(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);

            await GetById(id);
            await _collection.DeleteOneAsync(filter);
        }
    }
}
