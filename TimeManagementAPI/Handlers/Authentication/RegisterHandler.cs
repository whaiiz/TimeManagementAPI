using MediatR;
using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.Authentication;
using TimeManagementAPI.Models;
using TimeManagementAPI.Repositories.Interfaces;
using TimeManagementAPI.Utils;

namespace TimeManagementAPI.Handlers.Authentication
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, ResponseModel>
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

        public async Task<ResponseModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseModel()
            {
                Message = "User registered with success",
                StatusCode = 200
            };

            if ((await _userRepository.GetByUsername(request.User.Username)).Username == request.User.Username)
            {
                response.Message = "Username already exists";
                response.StatusCode = 400;
                return response;
            }

            if ((await _userRepository.GetByEmail(request.User.Email)).Email == request.User.Email)
            {
                response.Message = "Email already exists";
                response.StatusCode = 400;
                return response;
            }

            CreatePasswordHash(request.User.Password, out byte[] passwordHash, out byte[] passwordSalt);

            await _userRepository.Create(new UserModel()
            {
                Email = request.User.Email,
                Username = request.User.Username,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.Now
            });

            return response;
        }
    }
}
