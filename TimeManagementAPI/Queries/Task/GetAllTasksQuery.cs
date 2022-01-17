using MediatR;
using System.Collections.Generic;
using TimeManagementAPI.Models;

namespace TimeManagementAPI.Queries.Task
{
    public record GetAllTasksQuery() : IRequest<List<TaskModel>>;
}
