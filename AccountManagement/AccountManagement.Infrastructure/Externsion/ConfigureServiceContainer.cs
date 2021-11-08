using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AccountManagement.DB;
using AccountManagement.Persistence.Contract.Implementation;
using AccountManagement.Persistence.Implementation;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using AccountManagement.Infrastructure.Mappings;

namespace AccountManagement.Infrastructure.Externsion
{
    public static class ConfigureServiceContainer
    {
        public static void AddDbContext(this IServiceCollection serviceCollection,
             IConfiguration configuration, IConfigurationRoot configRoot)
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ?? configRoot["ConnectionStrings:DefaultConnection"])
                );
        }

        public static void AddAutoMapper(this IServiceCollection serviceCollection)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PersonProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            serviceCollection.AddSingleton(mapper);
        }

        public static void AddAddScopedServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            serviceCollection.AddScoped<IPersonService, PersonService>();
        }
             

        public static void AddController(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddControllers()
            .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        public static void AddVersion(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }        
    }
}
