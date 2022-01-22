﻿using System.Threading.Tasks;
using TimeManagementAPI.Models;

namespace TimeManagementAPI.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<UserModel>
    {
        Task<UserModel> GetByUsername(string username);
    }
}
