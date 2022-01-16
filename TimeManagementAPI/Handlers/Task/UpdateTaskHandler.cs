using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.Task;
using TimeManagementAPI.Repositories;

namespace TimeManagementAPI.Handlers.Task
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand>
    {
        private readonly TaskRepository _taskRepository;

        public UpdateTaskHandler(TaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            await _taskRepository.Update(request.Task);
            return Unit.Value;
        }
    }
}
