using MediatR;
using TimeManagementAPI.Models;

namespace TimeManagementAPI.Commands.Task
{
    public record UpdateTaskCommand(TaskModel Task) : IRequest;
}
