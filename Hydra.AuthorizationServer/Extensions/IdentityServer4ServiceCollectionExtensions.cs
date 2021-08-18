using Hydra.Shared;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace Hydra.AuthorizationServer.Extensions;
public static class IdentityServer4ServiceCollectionExtensions
{
    public static List<TestUser> TestUsers => new()
    {
        new TestUser
        {
            Username = "ArminShU",
            Password = "ArminShP",
            IsActive = true,
            SubjectId = "8080",
            Claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Email,"rmin@cyberservices.com"),
                new Claim(JwtClaimTypes.PhoneNumber,"0912"),
                new Claim(JwtClaimTypes.PhoneNumberVerified,"true", ClaimValueTypes.Boolean),
                new Claim(JwtClaimTypes.Name,"Armin Shoeibi"),
            }
        },
    };
    public static IServiceCollection AddIdentityServer4(this IServiceCollection services)
    {
        List<Client> clients = new()
        {
            new Client
            {
                ClientId = StaticDetails.HydraClientCredentialsFlowClientId,
                ClientSecrets = new List<Secret>
                {
                    new Secret(StaticDetails.HydraClientCredentialsFlowSecret.Sha256())
                },
                ClientName = "Hydra.ClientCredentialsFlow",
                ClientUri = "https://localhost:7001",
                RequireClientSecret = true,
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = new List<string>
                {
                    "test-scope"
                },

            },
            new Client
            {
                ClientId = StaticDetails.HydraAuthorizationCodeFlowClientId,
                ClientSecrets = new List<Secret>
                {
                    new Secret(StaticDetails.HydraAuthorizationCodeFlowSecret.Sha256())
                },
                AllowedGrantTypes = GrantTypes.Code,
                ClientName = "Hydra.AuthorizationCodeFlow",
                ClientUri = "https://localhost:6001",
                RequireConsent = true,
                AllowedScopes = new List<string>
                {
                    "test-scope"
                },
                RedirectUris = new List<string>()
                {
                    "https://localhost:6001/Home/GetToken"
                },
                RequirePkce = false,

            }
        };



        List<IdentityResource> identityResources = new()
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Phone(),
        };

        List<ApiScope> apiScopes = new()
        {
            new ApiScope("test-scope")
        };

        services.AddIdentityServer(identityServerOptions =>
        {
            identityServerOptions.Events.RaiseErrorEvents = true;
            identityServerOptions.Events.RaiseFailureEvents = true;
            identityServerOptions.Events.RaiseInformationEvents = true;
            identityServerOptions.Events.RaiseSuccessEvents = true;
            identityServerOptions.EmitStaticAudienceClaim = true;
        })
        .AddInMemoryClients(clients)
        .AddInMemoryApiScopes(apiScopes)
        .AddInMemoryIdentityResources(identityResources)
        .AddTestUsers(TestUsers)
        .AddDeveloperSigningCredential();

        return services;
    }
}
