using MediatR;

namespace TimeManagementAPI.Commands.Task
{
    public record UpdateTaskStatusCommand(int Id) : IRequest;
}
