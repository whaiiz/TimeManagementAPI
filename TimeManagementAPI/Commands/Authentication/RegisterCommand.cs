using MediatR;
using TimeManagementAPI.Utils;
using TimeManagementAPI.Models.Requests.Authentication;

namespace TimeManagementAPI.Commands.Authentication
{
    public record RegisterCommand(RegisterRequest Request) : IRequest<ResponseModel>;
}
