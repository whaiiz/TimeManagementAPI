using MediatR;

namespace TimeManagementAPI.Commands.Task
{
    public record DeleteTaskCommand(string Id) : IRequest;
}
