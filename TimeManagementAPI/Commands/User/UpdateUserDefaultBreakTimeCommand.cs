using MediatR;

namespace TimeManagementAPI.Commands.User
{
    public record UpdateUserDefaultBreakTimeCommand(string Username, int BreakTime) : IRequest;
}
