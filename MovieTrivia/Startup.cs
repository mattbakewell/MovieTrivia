using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using MovieTrivia.Application;

namespace MovieTrivia
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            /// Get the appsettings and environment AddEnvironmentVariables
            /// not that we're using them much at the moment, but we'll
            /// more than likely add some soon enough
            // TODO: Take this out before production is not used by deployment time
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
            // Configure the Dependency Injection Container
            services.AddDbContext<ApplicationDbContext>();
            services.AddTransient<TriviaService, TriviaService>();

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            // Add basic logging
            loggerFactory.AddDebug();

            // Add the MVC functionality
            app.UseMvc();

            // Migrate / Create / Seed our database
            using (var scope = app.ApplicationServices.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Seed();
            }
        }
    }
}
