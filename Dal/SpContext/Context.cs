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
      SpInfos = SpInfo.Read(SpMappers, ConStrManager.Get(Constant.APP));
    }

    private SpInfo GetSpInfo(OperationType op, string name) => SpInfos.FirstOrDefault(sp => sp.Op == op && sp.Name.IsEqual(name));

    public IRead<T> ReferenceData<T>(int appId = 0) where T : new()
    {
      var user = ConStrManager.GetAppUser(appId);
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
      var cfgs = config.GetSection(Constant.CONNECTIONSTRINGS)?.GetChildren();
      ConnectionStrings = (cfgs == null) ? throw new NullReferenceException() : cfgs.ToDictionary(s => s.Key, s => s.Value);
    }

    public string Get(string schema) => ConnectionStrings.First(s => s.Key.IsEqual(schema)).Value;

    public User GetUser(ClaimsPrincipal uc, int appId)
    {
      return new User(this, uc, appId);
    }

    public User GetAppUser(int appId)
    {
      return User.AppUser(this, appId);
    }

    internal sealed class User
    {
      public readonly int RootId;
      public readonly string ConnectionStr;

      internal User(IConnectionStringManager conStrMng, ClaimsPrincipal cp, int appId)
      {
        foreach (var c in cp.Claims)
        {
          switch (c.Type)
          {
            case "client_id":
              RootId = int.Parse(c.Value);
              break;

            case "role":
              ConnectionStr = conStrMng.Get(c.Value);
              break;
          }

          RootId = (RootId == 0) ? appId : 0;
        }
      }

      private User(string conStr, int appId)
      {
        ConnectionStr = conStr;
        RootId = appId;
      }

      internal static User AppUser(IConnectionStringManager conStrMng, int appId) => new User(conStrMng.Get(Constant.APP), appId);
    }
  }
}