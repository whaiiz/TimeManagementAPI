namespace TimeManagementAPI.Models.Responses
{
    public class LoginResponse
    {
        public int StatusCode { get; set; }

        public string Message { get; set; } = "";

        public string RefreshToken { get; set; }

        public string AccessToken { get; set; }

        public LoginResponse()
        {

        }

        public LoginResponse(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
