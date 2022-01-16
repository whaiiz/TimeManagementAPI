using MediatR;

namespace TimeManagementAPI.Commands.Task
{
    public record UpdateTaskDateCommand(int Id) : IRequest;
}
