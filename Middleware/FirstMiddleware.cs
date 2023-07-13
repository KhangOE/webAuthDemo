namespace web_authentication.Middleware
{
    public class FirstMiddleware : IMiddleware
    {       
       
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Response.Headers.Add("m1", "m1");
            context.Items.Add("data1 ", "data1");             
            if(context.Request.Path == "/m1")
            {
                await context.Response.WriteAsync("m11");
            }
            await next(context);
        }
    }
}
