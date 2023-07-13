using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace web_authentication.filter
{
    public class LoggingResultFilter : IResultFilter
    {
        private readonly ILogger<LoggingResultFilter> _logger;

        public LoggingResultFilter(ILogger<LoggingResultFilter> logger)
        {
           _logger = logger;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
             //Log the start of the action execution
            
           // var isAuthenticate = context.HttpContext.User.Identity.IsAuthenticated;
            var UserName = context.HttpContext.User.Identity.Name;
            Console.WriteLine("user :" + UserName);
            _logger.LogInformation("Action {ActionName} is executing.", context.ActionDescriptor.DisplayName);
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            // Log the end of the action execution and the result
            _logger.LogInformation("Action {ActionName} executed with result {Result}.", context.ActionDescriptor.DisplayName, context.Result);
        }
    }
}
