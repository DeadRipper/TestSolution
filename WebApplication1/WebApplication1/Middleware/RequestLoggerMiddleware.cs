namespace WebApplication1.Middleware
{
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"method: {context.Request.Method}\r\nurl: {context.Request.Path}");
            if (context.Request.Method == "GET")
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 405;
                await context.Response.WriteAsync("Only GET requests are allowed");
                return;
            }
        }
    }
}