using MediatR;

namespace TimeManagementAPI.Commands.Authentication
{
    public record LoginCommand(string Username, string Password) : IRequest<string>;

}
