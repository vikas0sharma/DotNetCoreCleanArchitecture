using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Retrospective.Infrastructure.Persistence;

namespace Retrospective.Infrastructure
{
    public static class DependencyRegister
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbConfiguration(configuration);
        }
    }
}
