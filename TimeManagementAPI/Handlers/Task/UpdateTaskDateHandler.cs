using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.Task;
using TimeManagementAPI.Repositories.Interfaces;

namespace TimeManagementAPI.Handlers.Task
{
    public class UpdateTaskDateHandler : IRequestHandler<UpdateTaskDateCommand>
    {
        private readonly ITaskRepository _taskRepository;

        public UpdateTaskDateHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        
        public async Task<Unit> Handle(UpdateTaskDateCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetById(request.Id);

            task.UpdatedAt = DateTime.Now;
            task.DateTime = request.DateTime;

            await _taskRepository.Update(task);
            return Unit.Value;
        }
    }
}
