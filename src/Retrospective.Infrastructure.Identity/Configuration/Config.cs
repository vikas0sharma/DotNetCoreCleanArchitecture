using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Retrospective.Infrastructure.Identity.Configuration
{
    public static class Config
    {
        // Identity resources are data like user ID, name, or email address of a user
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiResource> GetApiResources() =>
            new[]
            {
               new ApiResource("retrospective", "API") {Scopes = {"retrospective"}}
            };

        public static IEnumerable<ApiScope> GetScopes() =>
            new List<ApiScope>
            {
                new ApiScope("retrospective"),
            };

        public static IEnumerable<Client> GetClients(IConfiguration configuration) =>
            new List<Client>
            {
                new Client
                {
                    ClientId ="retrospective_swagger",
                    ClientName ="Retrospective Swagger",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    //AllowAccessTokensViaBrowser = true,
                    RedirectUris = { $"{configuration["Clients:RetroAPIUrl"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["Clients:RetroAPIUrl"]}/swagger/" },
                    AllowedCorsOrigins = { configuration["Clients:RetroAPIUrl"]  },
                    AllowedScopes =
                    {
                        "retrospective"
                    },

                    RequireConsent = false,
                    RequireClientSecret = false,
                }
            };
    }
}
