using MediatR;
using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.Authentication;
using TimeManagementAPI.Repositories.Interfaces;

namespace TimeManagementAPI.Handlers.Authentication
{
    public class GenerateRefreshTokenHandler : IRequestHandler<GenerateRefreshTokenCommand, string>
    {
        private readonly IUserRepository _userRepository;

        public GenerateRefreshTokenHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Handle(GenerateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            
            rng.GetBytes(randomNumber);
            request.User.RefreshToken = Convert.ToBase64String(randomNumber);
            request.User.RefreshTokenExpiryTime = DateTime.Now.AddDays(60);
            
            await _userRepository.Update(request.User);

            return request.User.RefreshToken;
        }
    }
}
