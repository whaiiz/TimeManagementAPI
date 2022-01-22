namespace TimeManagementAPI.Exceptions
{
    public class TaskNotFoundException : BaseException
    {
        public TaskNotFoundException() : base("Task not found", 404)
        {
        }

        public TaskNotFoundException(string message) : base(message, 404)
        {
        }
    }
}