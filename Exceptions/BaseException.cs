using System;

namespace TimeManagementAPI.Exceptions
{
    public abstract class BaseException : Exception
    {
        public int StatusCode { get; set; } = 500;

        public BaseException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
