using MediatR;
using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.Authentication;
using TimeManagementAPI.Models;
using TimeManagementAPI.Repositories.Interfaces;

namespace TimeManagementAPI.Handlers.Authentication
{
    public class RegisterHandler : IRequestHandler<RegisterCommand>
    {
        private readonly IUserRepository _userRepository;

        public RegisterHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            CreatePasswordHash(request.User.Password, out byte[] passwordHash, out byte[] passwordSalt);

            await _userRepository.Create(new UserModel()
            {
                Username = request.User.Username,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.Now
            });

            return Unit.Value;
        }
    }
}
