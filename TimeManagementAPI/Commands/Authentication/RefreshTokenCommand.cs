using MediatR;

namespace TimeManagementAPI.Commands.Authentication
{
    public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<bool>;
}
