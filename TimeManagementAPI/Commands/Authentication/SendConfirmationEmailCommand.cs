using MediatR;
using TimeManagementAPI.Models;

namespace TimeManagementAPI.Commands.Authentication
{
    public record SendConfirmationEmailCommand(UserModel User) : IRequest<bool>;
}
