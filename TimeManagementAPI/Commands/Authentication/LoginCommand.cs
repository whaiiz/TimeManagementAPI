using MediatR;
using TimeManagementAPI.Models.Responses;

namespace TimeManagementAPI.Commands.Authentication
{
    public record LoginCommand(string Username, string Password) : IRequest<LoginResponse>;
}
