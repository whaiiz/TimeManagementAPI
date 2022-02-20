using MediatR;
using TimeManagementAPI.Utils;

namespace TimeManagementAPI.Commands.Authentication
{
    public record ForgotPasswordCommand(string Email) : IRequest<ResponseModel>;
}
