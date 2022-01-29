using MediatR;
using TimeManagementAPI.Utils;

namespace TimeManagementAPI.Commands.Authentication
{
    public record ConfirmEmailCommand(string Token) : IRequest<ResponseModel>;
}
