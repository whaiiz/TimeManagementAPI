using System.ComponentModel.DataAnnotations;

namespace TimeManagementAPI.Models.Requests.Authentication
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "The username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The password is required")]
        public string Password { get; set; }
    }
}
