using MediatR;
using System.Security.Claims;
using TimeManagementAPI.Utils;

namespace TimeManagementAPI.Commands.Authentication
{
    public record ResetPasswordCommand(ClaimsPrincipal User, string NewPassword) : IRequest<ResponseModel>;
}
