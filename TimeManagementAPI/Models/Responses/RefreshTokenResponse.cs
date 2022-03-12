namespace TimeManagementAPI.Models.Responses
{
    public class RefreshTokenResponse
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public string Token { get; set; }

        public bool RefreshTokenExpired { get; set; }

        public string RefreshToken { get; set; }
        
        public string AccessToken { get; set; }
    }
}
