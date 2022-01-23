using MediatR;
using System.Security.Claims;
using TimeManagementAPI.Models;

namespace TimeManagementAPI.Commands.Task
{
    public record CreateTaskCommand(TaskModel Task, ClaimsPrincipal User) : IRequest<TaskModel>;
}
