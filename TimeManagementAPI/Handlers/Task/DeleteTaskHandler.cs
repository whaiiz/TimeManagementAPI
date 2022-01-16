﻿using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.Task;
using TimeManagementAPI.Repositories.Interfaces;

namespace TimeManagementAPI.Handlers.Task
{
    public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly ITaskRepository _taskRepository;

        public DeleteTaskHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            await _taskRepository.Delete(request.Id);
            return Unit.Value;
        }
    }
}
