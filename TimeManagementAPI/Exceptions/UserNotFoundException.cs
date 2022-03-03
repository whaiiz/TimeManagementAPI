namespace TimeManagementAPI.Exceptions
{
    public class UserNotFoundException : BaseException
    {
        public UserNotFoundException() : base("User not found", 404)
        {
        }

        public UserNotFoundException(string message) : base(message, 404)
        {
        }
    }
}
