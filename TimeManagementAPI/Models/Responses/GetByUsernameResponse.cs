namespace TimeManagementAPI.Models.Responses
{
    public class GetByUsernameResponse
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string EmailConfirmationToken { get; set; }

        public int DefaultFocusTime { get; set; }

        public int DefaultBreakTime { get; set; }
    }
}
