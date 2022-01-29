using MediatR;
using System.Collections.Generic;
using TimeManagementAPI.Models;

namespace TimeManagementAPI.Queries.Task
{
    public record GetTasksByUserQuery(string Username) : IRequest<ICollection<TaskModel>>;
}
