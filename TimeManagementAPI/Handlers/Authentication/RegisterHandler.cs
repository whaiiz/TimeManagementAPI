using MediatR;
using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.Authentication;
using TimeManagementAPI.Commands.Email;
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

        private async Task<bool> SendConfirmationEmail(UserModel user)
        {
            //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            using var hmac = new HMACSHA512();
            user.ConfirmationToken = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(user.));

            //await _userRepository.Update(user);
            return await _mediator.Send(new EmailSenderCommand(user.Email, 
                "Confirmation email", "www.youtube.com"));
        }
 
        public async Task<ResponseModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByUsername(request.User.Username) != null) 
                return new ResponseModel(400, "Username already exists");

            if (await _userRepository.GetByUsername(request.User.Email) != null)  
                return new ResponseModel(400, "Email already exists");

            CreatePasswordHash(request.User.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = await _userRepository.Create(new UserModel()
            {
                Email = request.User.Email,
                Username = request.User.Username,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.Now
            });

            if (!await SendConfirmationEmail(newUser)) 
                return new ResponseModel(400, "Error sending confirmation email, please try to log in");

            return new ResponseModel(200, 
                "User registered with success! Please go to your email to activate your account");
        }
    }
}
