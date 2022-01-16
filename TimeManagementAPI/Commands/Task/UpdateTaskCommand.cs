﻿using MediatR;

namespace TimeManagementAPI.Commands.Task
{
    public record UpdateTaskCommand(int Id) : IRequest;
}
