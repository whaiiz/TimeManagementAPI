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
        private readonly IMediator _mediator;

        public RegisterHandler(IUserRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
 
        public async Task<ResponseModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByUsername(request.User.Username) != null) 
                return new ResponseModel(400, Messages.UsernameAlreadyExists);

            if (await _userRepository.GetByUsername(request.User.Email) != null)  
                return new ResponseModel(400, Messages.EmailAlreadyExists);

            CreatePasswordHash(request.User.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = await _userRepository.Create(new UserModel()
            {
                Email = request.User.Email,
                Username = request.User.Username,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.Now
            });

            if (!await _mediator.Send(new SendConfirmationEmailCommand(newUser), cancellationToken))
                return new ResponseModel(400, Messages.ErrorSendingEmailConfirmationOnRegister);

            return new ResponseModel(200, Messages.UserRegisteredWithSuccess);
        }
    }
}
