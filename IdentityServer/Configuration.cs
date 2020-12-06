using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        public static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope> { 
                new ApiScope("ApiOne"),
            };

        public static IEnumerable<ApiResource> GetApis() =>
            new List<ApiResource> {
                new ApiResource("ApiOneRes"),
            };

        public static IEnumerable<Client> GetClients() =>
            new List<Client>{
                new Client()
                {
                    ClientId = "client_id",
                    ClientSecrets = {new Secret("client_secret".ToSha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    RedirectUris = { "https://oauth.pstmn.io/v1/browser-callback" },

                    AllowedScopes = {"ApiOne"},
                },

                new Client()
                {
                    ClientId = "angular_client",
                    ClientSecrets = {new Secret("angular_secret".ToSha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://oauth.pstmn.io/v1/browser-callback" },

                    AllowOfflineAccess = true,

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
