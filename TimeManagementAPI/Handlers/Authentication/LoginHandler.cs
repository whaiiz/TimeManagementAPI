using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.Authentication;
using TimeManagementAPI.Models;
using TimeManagementAPI.Models.Responses;
using TimeManagementAPI.Repositories.Interfaces;
using TimeManagementAPI.Utils;

namespace TimeManagementAPI.Handlers.Authentication
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public LoginHandler(IConfiguration configuration, IUserRepository userRepository,
            IMediator mediator)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _mediator = mediator;
        }

        private string GenerateToken(UserModel user)
        {
            // Add data to the token
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Username) };
            var key = new SymmetricSecurityKey(System.Text.Encoding
                .UTF8.GetBytes(_configuration.GetSection("Jwt:TokenKey").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims: claims, 
                expires: DateTime.Now.AddDays(1), signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        private async Task<string> GenerateRefreshToken(UserModel user)
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            user.RefreshToken = Convert.ToBase64String(randomNumber);
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(120);
            await _userRepository.Update(user);

            return user.RefreshToken;
        }

        private static bool IsPasswordCorrect(UserModel user, string passwordInput)
        {
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordInput));
            return computedHash.SequenceEqual(user.PasswordHash);
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsername(request.Username);

            if (user == null) return new LoginResponse(400, Messages.UserDoesNotExist);
            if (!IsPasswordCorrect(user, request.Password)) return new LoginResponse(400, Messages.WrongPassword);
            if (!user.IsEmailConfirmed) 
            {
                if (await _mediator.Send(new SendConfirmationEmailCommand(user), cancellationToken))
                {
                    return new LoginResponse(400, Messages.ConfirmYourEmail);
                }

                return new LoginResponse(400, Messages.ErrorSendingEmailConfirmationOnLogin);
            }

            return new LoginResponse()
            {
                StatusCode = 200,
                AccessToken = GenerateToken(user),
                RefreshToken = await GenerateRefreshToken(user),
            };
       }
    }
}
