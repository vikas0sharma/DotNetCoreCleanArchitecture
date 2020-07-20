using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Retrospective.Infrastructure.Identity.Configuration;
using Retrospective.Infrastructure.Identity.Data;
using Retrospective.Infrastructure.Identity.Models;
using System;
using System.Linq;
using System.Reflection;

namespace Retrospective.Infrastructure.Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            var connectionString = Configuration["ConnectionString"];
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            // EF core
            services.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlServer(connectionString,
                   sqlServerOptionsAction: sqlOptions =>
                   {
                       sqlOptions.MigrationsAssembly(migrationsAssembly);
                       sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                   }));

            // Identity
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Adds IdentityServer
            services.AddIdentityServer()
                .AddAspNetIdentity<AppUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddDeveloperSigningCredential();

            //services.AddAuthentication()
            //    .AddOpenIdConnect("oidc", "Identity Server", options =>
            //    {
            //        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
            //        options.SignOutScheme = IdentityServerConstants.SignoutScheme;
            //        options.SaveTokens = true;

            // options.ClientId = "interactive.confidential"; options.ClientSecret = "secret";
            // options.ResponseType = "code";

            // options.TokenValidationParameters = new TokenValidationParameters { NameClaimType =
            // "name", RoleClaimType = "role" }; });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InitializeDatabase(app);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();

                if (!context.Clients.Any())
                {
                    foreach (var client in Config.GetClients(Configuration))
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    foreach (var resource in Config.GetScopes())
                    {
                        context.ApiScopes.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
