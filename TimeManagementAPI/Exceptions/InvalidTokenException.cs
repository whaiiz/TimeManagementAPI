namespace TimeManagementAPI.Exceptions
{
    public class InvalidTokenException : BaseException
    {
        public InvalidTokenException() : base("Invalid token", 400)
        {
        }
    }
}
