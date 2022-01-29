using MediatR;
using TimeManagementAPI.Utils;
using TimeManagementAPI.Dtos;

namespace TimeManagementAPI.Commands.Authentication
{
    public record RegisterCommand(UserRegisterDto User) : IRequest<ResponseModel>;
}
