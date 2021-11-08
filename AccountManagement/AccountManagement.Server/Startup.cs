using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using AccountManagement.DB;
using Microsoft.EntityFrameworkCore;
using AccountManagement.Infrastructure.Externsion;
using AccountManagement.Infrastructure;
using Microsoft.Extensions.Logging;
using AccountManagement.Server.Installers;
using AccountManagement.Server.Options;
using AccountManagement.Persistence;

namespace AccountManagement.Server
{
    public class Startup
    {
        private readonly IConfigurationRoot configRoot;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            configRoot = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallServicesInAssembly(Configuration, configRoot);

            services.AddController();

            services.AddDbContext(Configuration, configRoot);

            services.AddAutoMapper();

            services.AddAddScopedServices();

            services.AddMediatorCQRS();

            services.AddVersion();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log, IServiceProvider service)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.ConfigureCustomExceptionMiddleware();

            app.UseCors(options =>
               options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });

            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
                //option.RoutePrefix = "OpenAPI";
            });

            RunMigrations(service);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void RunMigrations(IServiceProvider service)
        {
            // This returns the context.
            using var context = service.GetService<ApplicationDbContext>();
            context.Database.Migrate();
        }
    }
}
