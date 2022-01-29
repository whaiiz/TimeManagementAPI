using MediatR;
using TimeManagementAPI.Models;

namespace TimeManagementAPI.Queries.Task
{
    public record GetTaskByIdQuery(string Id, string Username) : IRequest<TaskModel>;
}
