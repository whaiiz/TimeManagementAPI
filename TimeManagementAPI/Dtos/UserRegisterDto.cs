using System.ComponentModel.DataAnnotations;

namespace TimeManagementAPI.Dtos
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "The username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The password is required")]
        public string Password { get; set; }
    }
}
