using MediatR;
using TimeManagementAPI.Dtos;

namespace TimeManagementAPI.Commands.Authentication
{
    public record RegisterCommand(UserDto User) : IRequest;
}
