
using System.Runtime.CompilerServices;

namespace MiddlewareExample.CustomMiddlewares
{
    public class MyCustomMiddleware : IMiddleware
    {
        async Task IMiddleware.InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("From Custom Middleware - Starts\n");
            await next(context);
            await context.Response.WriteAsync("From Custom Middleware - Ends\n");
        }
    }
    public class MyCustomMiddlewareWithoutIMiddleware
    {
        private readonly RequestDelegate next;
        public MyCustomMiddlewareWithoutIMiddleware(RequestDelegate _next)
        {
            next = _next;
        }
        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync("From Custom Middleware - Starts\n");
            await next(context);
            await context.Response.WriteAsync("From Custom Middleware - Ends\n");
        }
    }
    public static class CustomMiddlewareExtension
    {
        public static IApplicationBuilder UseMyCustomMiddleware
            (this IApplicationBuilder app)
        {
            //app.UseMiddleware<MyCustomMiddleware>();
            app.UseMiddleware<MyCustomMiddlewareWithoutIMiddleware>();
            return app;
        }
    }
}
