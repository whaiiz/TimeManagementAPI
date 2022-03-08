using MediatR;
using Microsoft.AspNetCore.Http;
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
using TimeManagementAPI.Models;
using TimeManagementAPI.Repositories.Interfaces;
using TimeManagementAPI.Utils;

namespace TimeManagementAPI.Handlers.Authentication
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, ResponseModel>
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public ForgotPasswordHandler(IConfiguration configuration, IUserRepository userRepository, 
            IMediator mediator)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _mediator = mediator;
        }

        private string GenerateTokenToResetPassword(UserModel user)
        {
            // Add data to the token
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Username) };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("JwtTokenKey").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<ResponseModel> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(request.Email);

            if (user == null) return new ResponseModel(404, Messages.UserDoesNotExist);

            var token = GenerateTokenToResetPassword(user);
            var forgotPasswordUrl = $"{_configuration.GetSection("FrontUrl").Value}/ResetPassword?token={token}";
            var message = File.ReadAllText("Utils/EmailViews/ResetPassword.html")
                .Replace("[forgotPasswordUrl]", forgotPasswordUrl);
            var success = await _mediator.Send(new EmailSenderCommand(user.Email, "Reset your password", message), cancellationToken);

            return success ? new ResponseModel(200, Messages.EmailSentToResetPassword) : 
                new ResponseModel(400, Messages.ErrorSendingEmailConfirmation);
        }
    }
}
