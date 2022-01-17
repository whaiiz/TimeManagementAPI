using MediatR;
using TimeManagementAPI.Models;

namespace TimeManagementAPI.Commands.Task
{
    public record CreateTaskCommand(TaskModel Task) : IRequest<TaskModel>;
}
