using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

using Microsoft.Extensions.Configuration;

namespace Dal.Sp
{
  public sealed class Context : IContext
  {
    private readonly IConnectionStringManager ConStrManager;
    private readonly ISpMappers SpMappers;
    private readonly IEnumerable<SpInfo> SpInfos;

    public Context(IConfiguration config, ISpMappers mappers)
    {
      ConStrManager = new ConnectionStringManager(config);
      SpMappers = mappers;
      SpInfos = SpInfo.Read(SpMappers, ConStrManager.App());
    }

    private SpInfo GetSpInfo(OperationType op, string name) => SpInfos.FirstOrDefault(sp => sp.Op == op && sp.Name.IsEqual(name));

    public IRead<T> ReferenceData<T>() where T : new()
    {
      var user = ConStrManager.GetAppUser();
      var spInfo = GetSpInfo(OperationType.R, typeof(T).Name);

      return new ReadOnly<T>(user, spInfo, SpMappers);
    }

    public IRead<T> ReadOnly<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : new()
    {
      var user = ConStrManager.GetUser(uc, appId);
      var spR = GetSpInfo(op, typeof(T).Name);

      return new ReadOnly<T>(user, spR, SpMappers);
    }

    public IWrite<T> ReadWrite<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : new()
    {
      var user = ConStrManager.GetUser(uc, appId);
      var spR = GetSpInfo(OperationType.R, typeof(T).Name);
      var spRW = GetSpInfo(op, typeof(T).Name);

      return new ReadWrite<T>(user, spRW, spR, SpMappers);
    }
  }

  internal sealed class ConnectionStringManager : IConnectionStringManager
  {
    private readonly IDictionary<string, string> ConnectionStrings;

    public ConnectionStringManager(IConfiguration config)
    {
      var cfg = config.GetSection(Constant.CONNECTIONSTRINGS);
      ConnectionStrings = (cfg == null) ? throw new NullReferenceException() : cfg.GetChildren().ToDictionary(s => s.Key, s => s.Value);
    }

    public string Get(string schema) => ConnectionStrings.First(s => s.Key.IsEqual(schema)).Value;

    public string App() => Get(Constant.APP);

    public User GetUser(ClaimsPrincipal uc, int appId)
    {
      var user = new User(uc, appId);
      user.ConnectionString = Get(user.Role);

      return user;
    }

    public User GetAppUser()
    {
      var user = User.AppUser();
      user.ConnectionString = App();

      return user;
    }

    internal sealed class User
    {
      public readonly int RootId;
      public readonly int AppId;
      private readonly bool IdVerified;

      internal readonly string Role;
      public string ConnectionString { get; internal set; }
      internal bool IsValid => IdVerified && !string.IsNullOrEmpty(ConnectionString);

      private User(string role)
      {
        Role = role;
        IdVerified = true;
      }

      internal User(ClaimsPrincipal cp, int appId)
      {
        AppId = appId;

        foreach (var c in cp.Claims)
        {
          switch (c.Type)
          {
            case "client_id":
              int clientId;
              RootId = int.TryParse(c.Value, out clientId) ? clientId : 0;
              break;

            case "role":
              Role = c.Value;
              break;

            case "id":
              int id;
              IdVerified = (int.TryParse(c.Value, out id) && appId == id);
              break;
          }
        }
      }

      internal static User AppUser() => new User(Constant.APP);
    }
  }
}