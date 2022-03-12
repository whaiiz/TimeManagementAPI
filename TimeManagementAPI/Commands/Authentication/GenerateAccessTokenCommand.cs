using MediatR;
using TimeManagementAPI.Models;

namespace TimeManagementAPI.Commands.Authentication
{
    public record GenerateAccessTokenCommand(UserModel User) : IRequest<string>;
}
