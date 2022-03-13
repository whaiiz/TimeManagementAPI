using MediatR;
using TimeManagementAPI.Models;

namespace TimeManagementAPI.Commands.Authentication
{
    public record GenerateRefreshTokenCommand(UserModel User) : IRequest<string>;
}
