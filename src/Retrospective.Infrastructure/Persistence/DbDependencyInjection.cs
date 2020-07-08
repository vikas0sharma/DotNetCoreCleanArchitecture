using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Retrospective.Domain.AggregatesModel.SprintAggregate;
using Retrospective.Infrastructure.Persistence.Repositories;
using System;
using System.Reflection;

namespace Retrospective.Infrastructure.Persistence
{
    public static class DbDependencyInjection
    {
        public static void AddDbConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ISprintRepository, SprintRepository>();

            services
            .AddDbContext<SprintContext>
            (options => options.UseSqlServer(configuration["ConnectionString"],
                sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsAssembly(typeof(SprintContext).GetTypeInfo().Assembly.GetName().Name);
                    sqlServerOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30),
                        null);
                }
            ));
        }
    }
}
