using MediatR;

namespace TimeManagementAPI.Commands.Task
{
    public record UpdateTaskStatusCommand(string Id, string Status) : IRequest;
}
