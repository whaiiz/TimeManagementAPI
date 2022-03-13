using System.Threading.Tasks;
using TimeManagementAPI.Models;

namespace TimeManagementAPI.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<UserModel>
    {
        Task<UserModel> GetByUsername(string username);

        Task<UserModel> GetByEmail(string email);

        Task<UserModel> GetByEmailConfirmationToken(string token);

        Task<UserModel> GetByRefreshToken(string refreshToken);
    }
}
