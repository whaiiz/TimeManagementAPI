using MediatR;
using TimeManagementAPI.Models;

namespace TimeManagementAPI.Commands.Task
{
    public record CreateTaskCommannd(int Id) : IRequest<TaskModel>;
}
