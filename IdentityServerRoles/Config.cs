using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Is4RoleDemo
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                new IdentityResources.OpenId(),
                //new IdentityResources.Email(),
                new IdentityResources.Profile(),
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

                new Client
                {
                    ClientId = "postman",
                    RequirePkce = true,
                    Enabled = true,

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    ClientSecrets = { new Secret("secret".ToSha256()) },

                    RedirectUris = { "https://oauth.pstmn.io/v1/browser-callback" },

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "scope1",
                        "scope2"
                    }
                },
                // m2m client credentials flow client
                new Client()
                {
                    ClientId = "client_id",
                    ClientSecrets = {new Secret("client_secret".ToSha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    RedirectUris = { "https://oauth.pstmn.io/v1/browser-callback" },

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
                                       "https://localhost:44334/signin-oidc", },


                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "ApiOne"
                    }
                },

            };
    }
}