using MediatR;
using System.Collections.Generic;
using TimeManagementAPI.Models;

namespace TimeManagementAPI.Queries.Task
{
    public record GetAllTasksQuery(int Id) : IRequest<List<TaskModel>>;
}
