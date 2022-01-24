﻿using System.ComponentModel.DataAnnotations;

namespace TimeManagementAPI.Dtos
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "The username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The password is required")]
        public string Password { get; set; }
    }
}
