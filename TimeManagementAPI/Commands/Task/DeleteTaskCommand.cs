using MediatR;
using System.Security.Claims;

namespace TimeManagementAPI.Commands.Task
{
    public record DeleteTaskCommand(string Id, string Username) : IRequest;
}
