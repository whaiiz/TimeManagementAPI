using MediatR;
using System;
using System.Linq;
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
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public LoginHandler(IUserRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
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
                AccessToken = await _mediator.Send(new GenerateAccessTokenCommand(user), cancellationToken),
                RefreshToken = await _mediator.Send(new GenerateRefreshTokenCommand(user), cancellationToken),
            };
        }
    }
}