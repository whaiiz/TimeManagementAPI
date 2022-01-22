using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Exceptions;
using TimeManagementAPI.Models;
using TimeManagementAPI.Queries.Task;
using TimeManagementAPI.Repositories.Interfaces;

namespace TimeManagementAPI.Handlers.Task
{
    public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, TaskModel>
    {
        private readonly ITaskRepository _taskRepository;

        public GetTaskByIdHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskModel> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _taskRepository.GetById(request.Id);

            if (result == null) throw new TaskNotFoundException();

            return result;
        }
    }
}
