using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServerRoles
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                // expanded version if more control is needed
                new ApiResource
                {
                    Name = "VetClinicApi",

                    // secret for using introspection endpoint
                    ApiSecrets =
                    {
                        new Secret("angular_secret".Sha256())
                    },
                    
                    // include the following using claims in access token (in addition to subject id)
                    UserClaims = { JwtClaimTypes.Name, JwtClaimTypes.Email },

                    // this API defines two scopes
                    Scopes =
                    {
                       "ApiOne",

                    },
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("scope1"),
                new ApiScope("scope2"),
                new ApiScope("ApiOne"),
            };

        public static IEnumerable<Client> Clients =>

            new Client[]
            {
                // m2m client credentials flow client
                new Client()
                {
                    ClientId = "client_id",
                    ClientSecrets = {new Secret("client_secret".ToSha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    RedirectUris = { "https://oauth.pstmn.io/v1/browser-callback",
                                    "https://localhost:44346/"},

                    AllowedScopes = {"ApiOne"},
                    RequireConsent = false,
                },

                new Client()
                {
                    ClientId = "angular_client",
                    RequirePkce = true,
                    ClientSecrets = {new Secret("angular_secret".ToSha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://oauth.pstmn.io/v1/browser-callback",
                                       "https://localhost:4999/signin-oidc", },

                    PostLogoutRedirectUris = { "https://localhost:4999/signout-callback-oidc"},


                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "ApiOne",
                    },

                    RequireConsent = true,

                    AllowOfflineAccess = true,

                    RefreshTokenUsage =  TokenUsage.OneTimeOnly,

                    AbsoluteRefreshTokenLifetime = 3600 * 72,
                    AccessTokenLifetime = 3600,

                    AccessTokenType = AccessTokenType.Reference,
                    
                },
            };
    }
}