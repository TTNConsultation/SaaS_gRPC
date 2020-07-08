// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
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
        new ApiResource
        {
          Name = "aaf",
          Properties = { new KeyValuePair<string,string>("appId", "6798" ) },
          ApiSecrets =
          {
            new Secret("apiSecret".Sha256())
          },
          UserClaims = { JwtClaimTypes.Role, JwtClaimTypes.Id },
          Scopes = { "aaf.user", "aaf.admin" }
        }
      };

    public static IEnumerable<ApiScope> ApiScopes =>
      new ApiScope[]
      {
        new ApiScope("aaf.user", "allaboutfood user"),
        new ApiScope("aaf.admin", "allaboutfood administrator")
      };

    public static IEnumerable<Client> Clients =>
      new Client[]
      {
        new Client
        {
            ClientId = "client",

            // no interactive user, use the clientid/secret for authentication
            AllowedGrantTypes = GrantTypes.ClientCredentials,

            // secret for authentication
            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },

            // scopes that client has access to
            AllowedScopes = { "aaf.admin" }
        },
        // interactive ASP.NET Core MVC client
        new Client
        {
            ClientId = "mvc",
            ClientSecrets = { new Secret("secret".Sha256()) },

            AllowedGrantTypes = GrantTypes.Code,
            // where to redirect to after login
            RedirectUris = { "https://localhost:5002/signin-oidc" },

            // where to redirect to after logout
            PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

            AllowOfflineAccess = true,

            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                "aaf.user"
            }
        },
        new Client
        {
            ClientId = "1",
            ClientName = "phokhanhhoa",
            ClientSecrets = { new Secret("secret".Sha256()) },

            AllowedGrantTypes = GrantTypes.Code,

            // where to redirect to after login
            RedirectUris = { "https://localhost:5005/signin-oidc" },

            // where to redirect to after logout
            PostLogoutRedirectUris = { "https://localhost:5005/signout-callback-oidc" },

            AllowOfflineAccess = true,

            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                "aaf.admin"
            }
        },
      };
  }
}