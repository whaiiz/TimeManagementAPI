using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.Task;
using TimeManagementAPI.Models;
using TimeManagementAPI.Repositories.Interfaces;

namespace TimeManagementAPI.Handlers.Task
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommannd, TaskModel>
    {
        private readonly ITaskRepository _taskRepository;

        public CreateTaskHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskModel> Handle(CreateTaskCommannd request, CancellationToken cancellationToken)
        {
            return await _taskRepository.Create(request.Task);
        }
    }
}
