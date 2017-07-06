using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Mvc.Cors.Internal;

namespace TemplateOfNetCoreWebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //add DbContext
            services.AddDbContext<SchoolContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //1.Enable SSL globally
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            //2. Enable CORS(Cross-Origin-Requests)
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://example.com", "http://www.other.com").AllowAnyMethod());

                //allow all origins
                //options.AddPolicy("AllowAllOrigins", builder =>
                //{
                //    builder.AllowAnyOrigin();
                //});
            });

            services.AddMvc();

            //2. globally configure Cors for every controller.
            //of course, we can apply [EnableCors] for controller or action
            //the precedence order is: Action-level > Controller level > Globally level
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowSpecificOrigin"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // DI SchoolContext into project.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, SchoolContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //1. redirect all http request to Https 
            var options = new RewriteOptions().AddRedirectToHttps();
            app.UseRewriter(options);

            //2.Enable CORS(Cross-Origin-Requests) - Shows UseCors with named policy
            app.UseCors("AllowSpecificOrigin");

            app.UseMvc();

            //create DB, should instead of Migration method
            //DbInitializer.Initialize(context);
        }
    }
}
