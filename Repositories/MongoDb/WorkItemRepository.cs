using TimeManagementAPI.Models;
using TimeManagementAPI.Repositories.Interfaces;
using TimeManagementAPI.Repositories.MongoDb;

namespace TimeManagementAPI.Repositories
{
    public class WorkItemRepository : Repository<WorkItem>, IWorkItemRepository
    {
    }
}
