namespace WebApplication1.Middleware
{
    public class UserAgentBlockerMiddleware
    {
        private readonly RequestDelegate _next;

        public UserAgentBlockerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("User-Agent", out var userAgent))
            {
                if (userAgent.ToString().IndexOf("curl", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("curl requests are not allowed");
                    return;
                }
            }

            await _next(context);
        }
    }
}