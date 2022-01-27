using MediatR;
using TimeManagementAPI.Utils;

namespace TimeManagementAPI.Commands.Authentication
{
    public record ConfirmEmailCommand(string Username, string Token) : IRequest<ResponseModel>;
}
