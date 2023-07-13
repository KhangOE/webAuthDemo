using System.Security.Claims;

namespace web_authentication.Middleware
{
    public class LoggingMiddleware : IMiddleware
    {
        private readonly ILogger _logger;
        
        public async Task InvokeAsync(HttpContext context,RequestDelegate next)
        {
            Console.WriteLine("path : " + context.Request.Path);
            Console.WriteLine($"Method : {context.Request.Method}");
           /* foreach(var h in context.Request.Headers)
            {
                Console.Write(h.Key + " : " + h.Value);
            }*/
            if (context.User.Identity.IsAuthenticated)
            {
                Console.WriteLine("user : " + context.User.Identity.Name);
                Console.WriteLine("Role : " +string.Join(",",context.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value)));
            }
            else
            {
                Console.WriteLine("Not authenticate");
            }
            await next(context);
        }
    }
}
