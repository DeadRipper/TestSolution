using System.Diagnostics;

namespace WebApplication1.Middleware
{
    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestTimingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sv = new Stopwatch();
            sv.Start();

            await _next(context);

            sv.Stop();
            Console.WriteLine($"{context.Request.Method} {context.Request.Path} took  {sv.ElapsedMilliseconds} ms");
        }
    }
}