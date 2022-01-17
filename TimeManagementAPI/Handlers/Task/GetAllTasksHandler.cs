using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Models;
using TimeManagementAPI.Queries.Task;
using TimeManagementAPI.Repositories.Interfaces;

namespace TimeManagementAPI.Handlers.Task
{
    public class GetAllTasksHandler : IRequestHandler<GetAllTasksQuery, ICollection<TaskModel>>
    {
        private readonly ITaskRepository _taskRepository;

        public GetAllTasksHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<ICollection<TaskModel>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            return await _taskRepository.GetAll();
        }
    }
}
