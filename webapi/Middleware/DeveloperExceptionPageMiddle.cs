using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Middleware
{
    public class DeveloperExceptionPageMiddle
    {
        public readonly RequestDelegate _next;
        public DeveloperExceptionPageMiddle(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

        }
    }
}
