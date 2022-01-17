using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.Task;
using TimeManagementAPI.Exceptions;
using TimeManagementAPI.Repositories.Interfaces;

namespace TimeManagementAPI.Handlers.Task
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand>
    {
        private readonly ITaskRepository _taskRepository;

        public UpdateTaskHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetById(request.Task.Id);

            if (task == null) throw new EntityNotFoundException();

            request.Task.UpdatedAt = DateTime.Now;

            await _taskRepository.Update(request.Task);

            return Unit.Value;
        }
    }
}
