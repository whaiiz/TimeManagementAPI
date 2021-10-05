namespace TimeManagementAPI.Exceptions
{
    public class EntityNotFoundException : BaseException
    {
        public EntityNotFoundException() : base("Entity not found", 404)
        {
        }

        public EntityNotFoundException(string message) : base (message, 404)
        {
        }
    }
}
