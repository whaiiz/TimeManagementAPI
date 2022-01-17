using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.Task;
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
            await _taskRepository.UpdateStatus(request.Id, request.Status);
            return Unit.Value;
        }
    }
}
