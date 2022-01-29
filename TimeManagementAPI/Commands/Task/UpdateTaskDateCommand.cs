using MediatR;
using System;

namespace TimeManagementAPI.Commands.Task
{
    public record UpdateTaskDateCommand(string Id, DateTime DateTime, string Username) : IRequest;
}
