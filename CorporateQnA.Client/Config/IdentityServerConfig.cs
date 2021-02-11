
using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace CorporateQnA.Client.Config
{
    public static class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };

        public static IEnumerable<ApiResource> GetApis() => new List<ApiResource>
        {
            new ApiResource("CorporateQnA"),
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<ApiScope> GetApiScopes() => new List<ApiScope>
        {
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityServer4.Models.Client> GetClients() => new List<IdentityServer4.Models.Client>
        {
            new IdentityServer4.Models.Client
            {
                ClientId ="angular",
                ClientSecrets = {new Secret("angular_secret".ToSha256())},
                RequirePkce = true,
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret=false,
                AllowedCorsOrigins =
                {
                    "http://localhost:4200"
                },
                RedirectUris =
                {
                    "http://localhost:4200",
                },
                PostLogoutRedirectUris =
                {
                    "http://localhost:4200"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "CorporateQnA",
                    IdentityServerConstants.LocalApi.ScopeName
                },
                AllowAccessTokensViaBrowser=true,
                RequireConsent=false,
                AlwaysIncludeUserClaimsInIdToken=true
            }
        };
    }
}
