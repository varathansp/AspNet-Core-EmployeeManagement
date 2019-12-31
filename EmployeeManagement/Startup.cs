using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Use(async (context,next) =>
            //{
            //    logger.LogInformation("M1 : request pipeline");
            //    await next();
            //    logger.LogInformation("M1 : response pipeline");
            //});

            //app.UseDefaultFiles();
            //app.UseStaticFiles();
            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("Home.html");
            //app.UseFileServer(fileServerOptions);
            app.UseStaticFiles();
            // app.UseMvcWithDefaultRoute();
            app.UseMvc(Routes=> {
                Routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            }); 

            app.Run(async (context) =>
            {
                //throw new Exception("Manual exception");
                await context.Response.WriteAsync("Request handled.. response started");
            });
        }
    }
}
