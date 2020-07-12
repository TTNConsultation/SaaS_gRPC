using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Dal.Sp
{
  public sealed class ConnectionManager : IConnectionManager
  {
    private readonly IDictionary<string, string> ConnectionStrings;

    public ConnectionManager(IConfiguration config)
    {
      var cfgs = config.GetSection(Constant.CONNECTIONSTRINGS)?.GetChildren();
      ConnectionStrings = (cfgs == null) ? throw new NullReferenceException() : cfgs.ToDictionary(s => s.Key, s => s.Value);
    }

    public string Get(string schema) => ConnectionStrings.First(s => s.Key.IsEqual(schema)).Value;

    public string App() => Get(Constant.APP);
  }

  public sealed class Context : IContext
  {
    private readonly IConnectionManager ConMgr;
    private readonly ISpMappers SpMappers;
    private readonly SpInfoManager SpInfoMgr;

    public Context(IConnectionManager conMgr, ISpMappers mappers)
    {
      ConMgr = conMgr;
      SpMappers = mappers;
      SpInfoMgr = new SpInfoManager(SpMappers, ConMgr.App());
    }

    public IRead<T> ReferenceData<T>(int appId = 0) where T : new()
    {
      var user = UserClaim.AppUser(ConMgr, appId);
      var spInfo = SpInfoMgr.Get(typeof(T).Name, OperationType.R);

      return new ReadOnly<T>(user, spInfo, SpMappers);
    }

    public IRead<T> ReadOnly<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : new()
    {
      var user = new UserClaim(ConMgr, uc, appId);
      var spR = SpInfoMgr.Get(typeof(T).Name, OperationType.R);

      return new ReadOnly<T>(user, spR, SpMappers);
    }

    public IWrite<T> ReadWrite<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : new()
    {
      var user = new UserClaim(ConMgr, uc, appId);

      var spR = SpInfoMgr.Get(typeof(T).Name, OperationType.R);
      var spRW = SpInfoMgr.Get(typeof(T).Name, op);

      return new ReadWrite<T>(user, spRW, spR, SpMappers);
    }
  }

  internal sealed class SpInfoManager
  {
    private readonly IEnumerable<SpInfo> SpInfos;

    internal SpInfo Get(string spName, OperationType op) => SpInfos.FirstOrDefault(sp => sp.Op == op && sp.Name.IsEqual(spName));

    public SpInfoManager(ISpMappers mappers, string conStr)
    {
      SpInfos = Read(mappers, conStr);
    }

    internal IEnumerable<SpInfo> Read(ISpMappers mappers, string conStr)
    {
      var parameters = ReadSpParameter(mappers, conStr);

      string spName = typeof(SpProperty).SpName(Constant.APP, nameof(OperationType.R));

      using var sqlcmd = new SqlCommand(spName, new SqlConnection(conStr))
      {
        CommandType = CommandType.StoredProcedure
      };

      sqlcmd.Connection.Open();
      using var reader = sqlcmd.ExecuteReader();

      var ret = new HashSet<SpInfo>();
      ISpMapper map = null;

      while (reader.Read())
      {
        var prop = (map == null) ? mappers.Add<SpProperty>(reader, out map) : map.Parse<SpProperty>(reader);
        var pars = parameters.Where(p => p.SpId == prop.Id);

        ret.Add(new SpInfo(prop, pars));
      }

      return ret;
    }

    private IEnumerable<SpParameter> ReadSpParameter(ISpMappers mappers, string conStr)
    {
      string spName = typeof(SpParameter).SpName(Constant.APP, nameof(OperationType.R));
      using var sqlcmd = new SqlCommand(spName, new SqlConnection(conStr))
      {
        CommandType = CommandType.StoredProcedure
      };
      sqlcmd.Connection.Open();

      using var reader = sqlcmd.ExecuteReader();
      return reader.Parse<SpParameter>(mappers);
    }
  }

  internal sealed class UserClaim
  {
    public readonly int RootId;
    public readonly string ConnectionString;
    public readonly string ErrorMessage = string.Empty;

    internal UserClaim(IConnectionManager conManager, ClaimsPrincipal cp, int appId)
    {
      foreach (var c in cp.Claims)
      {
        switch (c.Type)
        {
          case "client_id":
            RootId = int.Parse(c.Value);
            break;

          case "role":
            ConnectionString = conManager.Get(c.Value);
            break;
        }
      }

      if (RootId <= 0)
        RootId = appId;
    }

    private UserClaim(string conStr, int appId)
    {
      ConnectionString = conStr;
      RootId = appId;
    }

    internal static UserClaim AppUser(IConnectionManager conManager, int appId) => new UserClaim(conManager.App(), appId);
  }
}