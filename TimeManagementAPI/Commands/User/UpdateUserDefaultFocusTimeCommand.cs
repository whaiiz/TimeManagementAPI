using MediatR;

namespace TimeManagementAPI.Commands.User
{
    public record UpdateUserDefaultFocusTimeCommand(string Username, int FocusTime) : IRequest;
}
