namespace TimeManagementAPI.Exceptions
{
    public class UnauthorizedTaskAccessException : BaseException
    {
        public UnauthorizedTaskAccessException() : base("Unauthorized task access", 401)
        {
        }

        public UnauthorizedTaskAccessException(string message) : base(message, 401)
        {
        }
    }
}
