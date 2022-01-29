using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Dtos;
using TimeManagementAPI.Models;
using TimeManagementAPI.Queries.User;
using TimeManagementAPI.Repositories.Interfaces;

namespace TimeManagementAPI.Handlers.User
{
    public class GetUserByUsernameHandler : IRequestHandler<GetUserByUsernameQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByUsernameHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken) => 
            _mapper.Map<UserModel, UserDto>(await _userRepository.GetByUsername(request.Username));
    }
}
