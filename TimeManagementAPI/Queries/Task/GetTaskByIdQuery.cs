using MediatR;
using TimeManagementAPI.Models;

namespace TimeManagementAPI.Queries.Task
{
    public record GetTaskByIdQuery(string Id) : IRequest<TaskModel>;
}
