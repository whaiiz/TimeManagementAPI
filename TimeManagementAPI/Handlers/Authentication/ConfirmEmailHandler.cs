using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.Authentication;
using TimeManagementAPI.Repositories.Interfaces;
using TimeManagementAPI.Utils;

namespace TimeManagementAPI.Handlers.Authentication
{
    public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand, ResponseModel>
    {
        private readonly IUserRepository _userRepository;

        public ConfirmEmailHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResponseModel> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken) 
        {
            var user = await _userRepository.GetByEmailConfirmationToken(request.Token);

            if (user == null) return new ResponseModel(400, "Token not found!");

            user.IsEmailConfirmed = true;

            await _userRepository.Update(user);

            return new ResponseModel(200, "Email confirmed");
        }
    }
}
