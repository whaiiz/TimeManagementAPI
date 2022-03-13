using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.Authentication;

namespace TimeManagementAPI.Handlers.Authentication
{
    public class GenerateAccessTokenHandler : IRequestHandler<GenerateAccessTokenCommand, string>
    {
        private readonly IConfiguration _configuration;

        public GenerateAccessTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> Handle(GenerateAccessTokenCommand request, CancellationToken cancellationToken)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, request.User.Username) };
            var key = new SymmetricSecurityKey(System.Text.Encoding
                .UTF8.GetBytes(_configuration.GetSection("Jwt:TokenKey").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims: claims,
                expires: DateTime.Now.AddDays(1), signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return System.Threading.Tasks.Task.FromResult(jwt);
        }
    }
}
