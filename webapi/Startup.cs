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
using App.Metrics;

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

            #region Metrics监控配置
            string IsOpen = Configuration.GetSection("InfluxDB")["IsOpen"].ToLower();
            if (IsOpen == "true")
            {
                string database = Configuration.GetSection("InfluxDB")["DataBaseName"];
                string InfluxDBConStr = Configuration.GetSection("InfluxDB")["ConnectionString"];
                string app = Configuration.GetSection("InfluxDB")["app"];
                string env = Configuration.GetSection("InfluxDB")["env"];
                string username = Configuration.GetSection("InfluxDB")["username"];
                string password = Configuration.GetSection("InfluxDB")["password"];

                var uri = new Uri(InfluxDBConStr);

                var metrics = AppMetrics.CreateDefaultBuilder()
                .Configuration.Configure(
                options =>
                {
                    options.AddAppTag(app);
                    options.AddEnvTag(env);
                })
                .Report.ToInfluxDb(
                options =>
                {
                    options.InfluxDb.BaseUri = uri;
                    options.InfluxDb.Database = database;
                    options.InfluxDb.UserName = username;
                    options.InfluxDb.Password = password;
                    options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
                    options.HttpPolicy.FailuresBeforeBackoff = 5;
                    options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
                    options.FlushInterval = TimeSpan.FromSeconds(5);
                })
                .Build();

                services.AddMetrics(metrics);
                services.AddMetricsReportScheduler();
                services.AddMetricsTrackingMiddleware();
                services.AddMetricsEndpoints();

            }
            #endregion


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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime lifetime)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles(); //静态文件服务

            #region 注入Metrics
            string IsOpen = Configuration.GetSection("InfluxDB")["IsOpen"].ToLower();
            if (IsOpen == "true")
            {
                app.UseMetricsAllMiddleware();
                // Or to cherry-pick the tracking of interest
                app.UseMetricsActiveRequestMiddleware();
                app.UseMetricsErrorTrackingMiddleware();
                app.UseMetricsPostAndPutSizeTrackingMiddleware();
                app.UseMetricsRequestTrackingMiddleware();
                app.UseMetricsOAuth2TrackingMiddleware();
                app.UseMetricsApdexTrackingMiddleware();

                app.UseMetricsAllEndpoints();
                // Or to cherry-pick endpoint of interest
                app.UseMetricsEndpoint();
                app.UseMetricsTextEndpoint();
                app.UseEnvInfoEndpoint();
            }
            #endregion

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
            //app.UseRequestLogger();
            //app.UseMyMiddleware();


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
