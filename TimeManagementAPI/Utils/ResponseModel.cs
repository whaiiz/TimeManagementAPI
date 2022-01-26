namespace TimeManagementAPI.Utils
{
    public class ResponseModel
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public ResponseModel(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
