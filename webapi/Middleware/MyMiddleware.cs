using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Middleware
{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;
        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            Start(context);
            await _next.Invoke(context);
            End(context);
        }

        private void Start(HttpContext context)
        {
            context.Response.WriteAsync("diy middle start");
        }

        private void End(HttpContext context)
        {
            context.Response.WriteAsync("diy middle end");
        }
    }


    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleware>();
        }
    }

}
