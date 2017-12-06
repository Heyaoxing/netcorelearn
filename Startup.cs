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

namespace webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles(); //静态文件服务

            //自定义中间件
            app.UseRequestLogger();

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
