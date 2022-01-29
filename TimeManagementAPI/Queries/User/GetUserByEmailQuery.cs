using MediatR;
using TimeManagementAPI.Dtos;

namespace TimeManagementAPI.Queries.User
{
    public record GetUserByEmailQuery(string Email) : IRequest<UserDto>;
}
