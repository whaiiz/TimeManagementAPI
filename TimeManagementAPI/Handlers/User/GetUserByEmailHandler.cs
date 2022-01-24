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
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByEmailHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken) => 
            _mapper.Map<UserModel, UserDto>(await _userRepository.GetByEmail(request.Email));
    }
}
