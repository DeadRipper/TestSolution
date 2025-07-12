namespace WebApplication1.Middleware
{
    public class AddCustomHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public AddCustomHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            context.Response.Headers["X-Powered-By"] = "AddCustomHeaderMiddleware";
        }
    }
}