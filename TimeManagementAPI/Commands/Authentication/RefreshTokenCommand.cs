using MediatR;
using TimeManagementAPI.Models.Responses;

namespace TimeManagementAPI.Commands.Authentication
{
    public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<RefreshTokenResponse>;
}
