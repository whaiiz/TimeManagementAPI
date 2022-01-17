using MediatR;
using TimeManagementAPI.Models;

namespace TimeManagementAPI.Commands.Task
{
    public record CreateTaskCommannd(TaskModel Task) : IRequest<TaskModel>;
}
