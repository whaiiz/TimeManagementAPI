using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using TimeManagementAPI.Exceptions;

namespace TimeManagementAPI.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute 
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var errorCode = 500;
            var message = "Something went wrong!";

            if (context.Exception is BaseException exception)
            {
                errorCode = exception.StatusCode;
                message = exception.Message;
            }

            context.Result = new ObjectResult(message)
            {
                StatusCode = errorCode
            };

            base.OnExceptionAsync(context);
            return Task.CompletedTask;
        }
    }
}
