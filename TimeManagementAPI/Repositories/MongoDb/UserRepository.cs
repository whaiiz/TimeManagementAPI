﻿using MongoDB.Driver;
using System.Threading.Tasks;
using TimeManagementAPI.Models;
using TimeManagementAPI.Repositories.Interfaces;

namespace TimeManagementAPI.Repositories.MongoDb
{
    public class UserRepository : Repository<UserModel>, IUserRepository
    {
        public UserRepository(IMongoCollection<UserModel> collection) : base(collection)
        {
        }

        public async Task<UserModel> GetByUsername(string username)
        {
            var filter = Builders<UserModel>.Filter.Eq("Username", username);
            var result = await Collection.Find(filter).FirstOrDefaultAsync();

            return result;
        }
    }
}
