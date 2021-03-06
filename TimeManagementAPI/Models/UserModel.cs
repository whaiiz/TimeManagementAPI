using System;

namespace TimeManagementAPI.Models
{
    public class UserModel : BaseModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string EmailConfirmationToken { get; set; }

        public byte[] PasswordHash { get; set; }

        // Technique used to help protect passwords stored in a database from being
        // reverse-engineered by hackers who might breach the environment.
        public byte[] PasswordSalt { get; set; }

        public int DefaultFocusTime { get; set; }

        public int DefaultBreakTime { get; set; }

        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
