using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Models;
using TimeManagementAPI.Queries.Task;
using TimeManagementAPI.Repositories;

namespace TimeManagementAPI.Handlers.Task
{
    public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, TaskModel>
    {
        private readonly TaskRepository _taskRepository;

        public GetTaskByIdHandler(TaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskModel> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            return await _taskRepository.GetById(request.Id);
        }
    }
}
