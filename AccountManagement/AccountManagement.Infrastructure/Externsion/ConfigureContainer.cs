using AccountManagement.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;

namespace AccountManagement.Infrastructure.Externsion
{
    public static class ConfigureContainer
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }

        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/OpenAPISpecification/swagger.json", "Account Management API");
                setupAction.RoutePrefix = "OpenAPI";
            });
        }
    }
}
