namespace web_authentication.Middleware
{
    public static class ExtensionMiddlewares
    {
        public static IApplicationBuilder UseFirstMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FirstMiddleware>();
        }

        public static IApplicationBuilder UseSecondMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SecondMiddleware>();
        }

        public static IApplicationBuilder UseLoggingMiddlware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}
