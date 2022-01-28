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
using TimeManagementAPI.Repositories.Interfaces;
using TimeManagementAPI.Utils;

namespace TimeManagementAPI.Handlers.Authentication
{
    public class LoginHandler : IRequestHandler<LoginCommand, ResponseModel>
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
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("JwtTokenKey").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private static bool IsPasswordCorrect(UserModel user, string passwordInput)
        {
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordInput));
            return computedHash.SequenceEqual(user.PasswordHash);
        }

        public async Task<ResponseModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsername(request.Username);

            if (user == null) return new ResponseModel(400, "User doesn't exist");
            if (!IsPasswordCorrect(user, request.Password)) return new ResponseModel(400, "Wrong Password");
            if (!user.IsEmailConfirmed) {
                if (await _mediator.Send(new SendConfirmationEmailCommand(user), cancellationToken))
                {
                    return new ResponseModel(400, "Confirm your email first");
                }

                return new ResponseModel(400, "There was an error sending your confirmation email! Please contact support");
            }


            return new ResponseModel(200, GenerateToken(user));
        }
    }
}
