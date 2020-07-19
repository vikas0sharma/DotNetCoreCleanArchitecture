using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Retrospective.API.Filters;
using System;
using System.Collections.Generic;

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

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://localhost:5000/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:5000/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {"retrospective", "Demo API - full access"}
                            }
                        }
                    }
                });
                c.OperationFilter<AuthorizeCheckOperationFilter>();
            });
        }

        public static void UseAPISwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Retrospective API V1");
                c.OAuthClientId("retrospective_swagger");
                c.OAuthAppName("Retrospective Swagger");
            });
        }
    }
}
