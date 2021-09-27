﻿using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            return await _collection.Find(new BsonDocument())
                .ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            return await _collection.Find(filter).FirstAsync();
        }

        public async Task Update(T entity)
        {
            await _collection.ReplaceOneAsync(
                new BsonDocument("Id", entity.Id),
                entity);
        }

        public async Task Delete(int id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            await _collection.DeleteOneAsync(filter);
        }
    }
}
