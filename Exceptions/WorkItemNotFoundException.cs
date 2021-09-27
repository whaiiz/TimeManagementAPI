using System;

namespace TimeManagementAPI.Exceptions
{
    public class WorkItemNotFoundException : Exception
    {
        public WorkItemNotFoundException()
        {
        }

        public WorkItemNotFoundException(string message)
            : base(message)
        {
        }

        public WorkItemNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
