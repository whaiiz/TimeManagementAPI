using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.Task;
using TimeManagementAPI.Exceptions;
using TimeManagementAPI.Repositories.Interfaces;

namespace TimeManagementAPI.Handlers.Task
{
    public class UpdateTaskStatusHandler : IRequestHandler<UpdateTaskStatusCommand>
    {
        private readonly ITaskRepository _taskRepository;

        public UpdateTaskStatusHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Unit> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetById(request.Id);

            if (task == null) throw new TaskNotFoundException();
            if (task.Username != request.Username) throw new UnauthorizedTaskAccessException();

            task.UpdatedAt = DateTime.Now;
            task.Status = request.Status;
            task.Username = request.Username;

            await _taskRepository.Update(task);
            return Unit.Value;
        }
    }
}
