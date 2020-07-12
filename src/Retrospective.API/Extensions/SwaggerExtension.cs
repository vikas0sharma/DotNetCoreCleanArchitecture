using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace Retrospective.API.Extensions
{
    public static class SwaggerExtension
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Retrospective API",
                    Description = "A project ASP.NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Vikas Sharma",
                        Email = "mailbox.viksharma@gmail.com",
                        Url = new Uri("https://twitter.com/vikas5harma"),
                    }
                });
            });
        }

        public static void AddSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Retrospective API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
