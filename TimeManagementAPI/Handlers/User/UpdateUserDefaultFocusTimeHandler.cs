using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.User;
using TimeManagementAPI.Exceptions;
using TimeManagementAPI.Repositories.Interfaces;

namespace TimeManagementAPI.Handlers.User
{
    public class UpdateUserDefaultFocusTimeHandler : IRequestHandler<UpdateUserDefaultFocusTimeCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserDefaultFocusTimeHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateUserDefaultFocusTimeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsername(request.Username);

            if (user == null) throw new UserNotFoundException();

            user.DefaultFocusTime = request.FocusTime;
            await _userRepository.Update(user);

            return Unit.Value;
        }
    }
}
