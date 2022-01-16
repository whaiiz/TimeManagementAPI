using MediatR;
using TimeManagementAPI.Models;

namespace TimeManagementAPI.Queries.Task
{
    public record GetTaskByIdQuery(int Id) : IRequest<TaskModel>;
}
