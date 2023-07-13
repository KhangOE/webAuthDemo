namespace web_authentication.Middleware
{
    public class SecondMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var data1 = context.Items["data1"];
            // Console.WriteLine("header from m1 : " + context.Response.Headers["m1"]);
               Console.WriteLine("data from m1 : " + data1);
               context.Response.Headers["m2"] = "m2";

            context.Items.Add("data2 ", "data2");
            if (context.Request.Path == "/m2")
            {
                await context.Response.WriteAsync("<h1>m22</h1>");
            }
            await next(context);
        }
    }
}
