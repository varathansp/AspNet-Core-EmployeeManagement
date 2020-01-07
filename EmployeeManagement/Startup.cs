using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            services.AddDbContextPool<AppDbContext>(options=>
            options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 7;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            });

            services.AddAuthentication().AddGoogle(options=> {
                options.ClientId = "920801116425-jktvgflc7ll151oofsf6jpvvqsc6ebi0.apps.googleusercontent.com";
                options.ClientSecret = "zKBaqTn5gLpMWIo6TjuLJjCt";
            });

            services.ConfigureApplicationCookie(options =>
            {
                
                options.AccessDeniedPath = new PathString("/Administration/AccessDenied");
            });

            services.AddAuthorization(options=> {
                options.AddPolicy("DeleteRolePolicy", policy => policy.RequireClaim("Delete Role").RequireRole("Admin"));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("EditDeleteRolePolicy", policy => policy.RequireClaim("Edit Role").RequireClaim("Delete Role"));
            });
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                // app.UseStatusCodePages();
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

            app.UseAuthentication();

            // app.UseMvcWithDefaultRoute();
            app.UseMvc(Routes=> {
                Routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            //app.Run(async (context) =>
            //{
            //    //throw new Exception("Manual exception");
            //    await context.Response.WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
            //});
        }
    }
}
