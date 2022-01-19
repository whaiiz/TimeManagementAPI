using MongoDB.Driver;
using TimeManagementAPI.Models;
using TimeManagementAPI.Repositories.Interfaces;

namespace TimeManagementAPI.Repositories.MongoDb
{
    public class UserRepository : Repository<UserModel>, IUserRepository
    {
        public UserRepository(IMongoCollection<UserModel> collection) : base(collection)
        {
        }
    }
}
