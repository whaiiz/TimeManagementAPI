using MediatR;

namespace TimeManagementAPI.Commands.Authentication
{
    public record RevokeTokenCommand(string RefreshToken) : IRequest;
}
