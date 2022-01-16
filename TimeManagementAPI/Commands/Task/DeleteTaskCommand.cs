using MediatR;

namespace TimeManagementAPI.Commands.Task
{
    public record DeleteTaskCommand(int Id) : IRequest;
}
