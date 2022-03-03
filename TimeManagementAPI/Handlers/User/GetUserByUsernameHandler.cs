using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Exceptions;
using TimeManagementAPI.Models;
using TimeManagementAPI.Models.Responses;
using TimeManagementAPI.Queries.User;
using TimeManagementAPI.Repositories.Interfaces;

namespace TimeManagementAPI.Handlers.User
{
    public class GetUserByUsernameHandler : IRequestHandler<GetUserByUsernameQuery, GetByUsernameResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByUsernameHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetByUsernameResponse> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsername(request.Username);

            if (user == null) throw new UserNotFoundException();

            return _mapper.Map<UserModel, GetByUsernameResponse>(user);
        }
    }
}
