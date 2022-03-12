using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.Authentication;
using TimeManagementAPI.Exceptions;
using TimeManagementAPI.Repositories.Interfaces;

namespace TimeManagementAPI.Handlers.Authentication
{
    public class RevokeTokenHandler : IRequestHandler<RevokeTokenCommand>
    {
        private readonly IUserRepository _userRepository;

        public RevokeTokenHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByRefreshToken(request.RefreshToken);
            
            if (user == null) throw new UserNotFoundException();
            
            user.RefreshToken = request.RefreshToken;
            
            await _userRepository.Update(user);

            return Unit.Value;
        }
    }
}
