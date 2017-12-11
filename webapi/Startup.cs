using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using webapi.Middleware;
using webapi.Config;
using Microsoft.AspNetCore.Http;

namespace webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // 配置用于应用程序内的服务,这些服务需要在接入请求管道之前先被加入 ConfigureServices 中
        public void ConfigureServices(IServiceCollection services)
        {

            //配置文件注册
            services.Configure<MyConfig>(Configuration.GetSection("MyConfig"));

            //使用Action配置
            //services.Configure<MyConfig>(p =>
            //{
            //    p.Age = 23;
            //    p.Name = "王大椎";
            //});

            //使用PostConfigure方法配置 PostConfigure 方法在 Configure 方法之后执行，是2.0中新增加的
            //services.PostConfigure<MyConfig>(p =>
            //{
            //    p.Age = 23;
            //    p.Name = "王大椎";
            //});


            services.AddMvc();

        }

        // 用于指定 ASP.NET 应用程序将如何响应每一个 HTTP 请求
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles(); //静态文件服务


            //当使用了 Map，每个请求所匹配的路径段将从 HttpRequest.Path 中移除，并附加到 HttpRequest.PathBase 中
            //app.Map("/test", (context) =>
            // {
            //     context.Run(async c =>
            //     {
            //         await c.Response.WriteAsync("map path!");
            //     });
            // });

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello world step1\n");
            //    await next.Invoke();
            //    await context.Response.WriteAsync("Hello world step1 Return\n");
            //});

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello world step2\n");
            //    await next.Invoke();
            //    await context.Response.WriteAsync("Hello world step2 Return\n");
            //});

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello, World Over!\n");//手工高亮
            //});




            //自定义中间件
            app.UseRequestLogger();
            app.UseMyMiddleware();


            //默认首页设置
            //请求文件夹的时候将检索以下文件：
            //default.htm
            //default.html
            //index.htm
            //index.html
            app.UseDefaultFiles();

            app.UseMvc();

        }
    }
}
