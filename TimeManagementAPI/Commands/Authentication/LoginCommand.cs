using MediatR;
using TimeManagementAPI.Utils;

namespace TimeManagementAPI.Commands.Authentication
{
    public record LoginCommand(string Username, string Password) : IRequest<ResponseModel>;

}
