using MediatR;
using TimeManagementAPI.Dtos;

namespace TimeManagementAPI.Queries.User
{
    public record GetUserByUsernameQuery(string Username) : IRequest<UserDto>;

}
