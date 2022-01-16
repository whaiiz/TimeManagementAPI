using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.Task;
using TimeManagementAPI.Repositories;

namespace TimeManagementAPI.Handlers.Task
{
    public class UpdateTaskStatusHandler : IRequestHandler<UpdateTaskStatusCommand>
    {
        private readonly TaskRepository _taskRepository;

        public UpdateTaskStatusHandler(TaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Unit> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
        {
            await _taskRepository.UpdateStatus(request.Id, request.Status);
            return Unit.Value;
        }
    }
}
