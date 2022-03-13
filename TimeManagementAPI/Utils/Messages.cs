﻿namespace TimeManagementAPI.Utils
{
    public static class Messages
    {
        /* Generic messages */
        public const string UnexpectedError = "Something went wrong!";
        public const string ErrorSendingEmailConfirmation = "Error sending email confirmation! Please contact support.";
        public const string PasswordReset = "Password reset done";
        
        /* Email confirmation messages */
        public const string EmailConfirmed = "Your email was confirmed with success!";
        public const string EmailConfirmation = "Email Confirmation";
        public const string InvalidEmailConfirmationToken = "The token sent is invalid or it has already expired!";

        /* Login messages */
        public const string UserDoesNotExist = "The user doesn't exist!";
        public const string WrongPassword = "Wrong password!";
        public const string ConfirmYourEmail = "Confirm your email first!";
        public const string ErrorSendingEmailConfirmationOnLogin = "Error sending email confirmation! Please contact support.";
        public const string EmailSentToResetPassword = "It was sent an email to reset your password";

        /* Register messages */
        public const string UsernameAlreadyExists = "Username already exists!";
        public const string EmailAlreadyExists = "Email already exists!";
        public const string ErrorSendingEmailConfirmationOnRegister = "User registered with success! but there was an " +
            "error sending email confirmation! Please try to log in.";
        public const string UserRegisteredWithSuccess = "User registered with success! Please go to your email to activate your account.";
    
        /* Token messages */
        public const string InvalidToken = "Invalid token!";
        public const string RefreshTokenExpired = "Refresh token expired.";

    }
}
