using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.Task;
using TimeManagementAPI.Repositories;

namespace TimeManagementAPI.Handlers.Task
{
    public class UpdateTaskDateHandler : IRequestHandler<UpdateTaskDateCommand>
    {
        private readonly TaskRepository _taskRepository;

        public UpdateTaskDateHandler(TaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        
        public async Task<Unit> Handle(UpdateTaskDateCommand request, CancellationToken cancellationToken)
        {
            await _taskRepository.UpdateDate(request.Id, request.DateTime);
            return Unit.Value;
        }
    }
}
