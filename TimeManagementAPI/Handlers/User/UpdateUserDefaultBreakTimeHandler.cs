using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.User;
using TimeManagementAPI.Exceptions;
using TimeManagementAPI.Repositories.Interfaces;

namespace TimeManagementAPI.Handlers.User
{
    public class UpdateUserDefaultBreakTimeHandler : IRequestHandler<UpdateUserDefaultBreakTimeCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserDefaultBreakTimeHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateUserDefaultBreakTimeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsername(request.Username);

            if (user == null) throw new UserNotFoundException();

            user.DefaultBreakTime = request.BreakTime;
            await _userRepository.Update(user);

            return Unit.Value;
        }
    }
}
