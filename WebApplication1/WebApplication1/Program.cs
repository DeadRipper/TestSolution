using System.Diagnostics;
using WebApplication1.Middleware;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();

            //app.UseRouting();

            //app.UseAuthorization();

            //app.MapRazorPages();

            app.UseMiddleware<RequestLoggerMiddleware>();
            app.UseMiddleware<UserAgentBlockerMiddleware>();
            app.UseMiddleware<RequestTimingMiddleware>();
            app.UseMiddleware<AddCustomHeaderMiddleware>();

            // 🔧 Custom Middleware to log request path
            app.Use(async (context, next) =>
            {
                Console.WriteLine($"Incoming request: {context.Request.Path}");
                await next();
            });
            // 🔧 Middleware to measure processing time
            app.Use(async (context, next) =>
            {
                var sv = new Stopwatch();
                sv.Start();

                await next();

                sv.Stop();
                Console.WriteLine($"Request processed in {sv.ElapsedMilliseconds} ms");
            });

            app.Run();
        }
    }
}