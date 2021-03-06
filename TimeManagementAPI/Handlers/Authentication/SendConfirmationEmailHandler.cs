using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.Authentication;
using TimeManagementAPI.Commands.Email;
using TimeManagementAPI.Repositories.Interfaces;
using TimeManagementAPI.Utils;

namespace TimeManagementAPI.Handlers.Authentication
{
    public class SendConfirmationEmailHandler : IRequestHandler<SendConfirmationEmailCommand, bool>
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public SendConfirmationEmailHandler(IConfiguration configuration, IUserRepository userRepository,
            IMediator mediator)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _mediator = mediator;
        }

        private string GenerateConfirmationToken(string username)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:TokenKey").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<bool> Handle(SendConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            var confirmationToken = GenerateConfirmationToken(request.User.Username);
            var emailConfirmationUrl = $"{_configuration.GetSection("BaseUrl").Value}/api/Authentication/ConfirmEmail?token={confirmationToken}";
            var message = File.ReadAllText("Utils/EmailViews/ConfirmEmail.html");
            message = message.Replace("[emailConfirmationUrl]", emailConfirmationUrl);

            request.User.EmailConfirmationToken = GenerateConfirmationToken(request.User.Username);
            
            await _userRepository.Update(request.User);
            return await _mediator.Send(new EmailSenderCommand(request.User.Email, Messages.EmailConfirmation, message), cancellationToken);
        }
    }
}
