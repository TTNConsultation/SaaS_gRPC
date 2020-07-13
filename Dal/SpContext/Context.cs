using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Claims;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Dal.Sp
{
  public interface IConnectionManager
  {
    string Get(string schema);

    string App();
  }

  public interface ICollectionMapper
  {
    IMapper Get(string typename);

    IMapper Get<T>() => Get(typeof(T).Name);
  }

  public interface IMapper
  {
    bool IsType(string typename);

    T Parse<T>(SqlDataReader reader) where T : new();
  }

  public interface ICollectionSpInfo
  {
    ISpInfo Get(string typename, OperationType op);
  }

  public interface IContext
  {
    IReadOnly<T> ReferenceData<T>(int rootId = 0) where T : new();

    IReadOnly<T> ReadOnly<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : new();

    IWrite<T> Write<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : new();
  }

  public sealed class ConnectionManager : IConnectionManager
  {
    private readonly IDictionary<string, string> ConnectionStrings;

    public ConnectionManager(IConfiguration config)
    {
      ConnectionStrings = config.GetSection(Constant.CONNECTIONSTRINGS)
                                ?.GetChildren()
                                ?.ToDictionary(s => s.Key, s => s.Value);
    }

    public string Get(string schema) => ConnectionStrings.FirstOrDefault(s => s.Key.IsEqual(schema)).Value;

    public string App() => Get(Constant.APP);
  }

  public sealed class CollectionMapper : ICollectionMapper
  {
    private readonly HashSet<IMapper> mappers = new HashSet<IMapper>();

    private IMapper Add(IMapper map) => mappers.Add(map) ? map : null;

    public IMapper Get(string typename) => mappers.FirstOrDefault(m => m.IsType(typename)) ?? Add(new Mapper(typename));
  }

  public sealed class Mapper : IMapper
  {
    private IDictionary<int, PropertyInfo> ReflectionDictionnary;
    private readonly string TypeName;

    internal Mapper(string typename)
    {
      TypeName = typename;
    }

    private T BuildDictionary<T>(SqlDataReader reader) where T : new()
    {
      ReflectionDictionnary = new Dictionary<int, PropertyInfo>();

      var propInfos = typeof(T).GetProperties();
      var ret = new T();

      for (int i = 0; i < reader.FieldCount; i++)
      {
        var propInfo = propInfos?.FirstOrDefault(pi => pi.Name.IsEqual(reader.GetName(i)));
        if (propInfo != null)
        {
          propInfo.SetValue(ret, Convert.ChangeType(reader[i], propInfo.PropertyType));
          ReflectionDictionnary.Add(new KeyValuePair<int, PropertyInfo>(i, propInfo));
        }
      }

      return ret;
    }

    private T UseDictionary<T>(SqlDataReader reader) where T : new()
    {
      var ret = new T();
      foreach (var kpv in ReflectionDictionnary)
      {
        kpv.Value.SetValue(ret, Convert.ChangeType(reader[kpv.Key], kpv.Value.PropertyType));
      }
      return ret;
    }

    public T Parse<T>(SqlDataReader reader) where T : new() =>
      (ReflectionDictionnary == null) ? BuildDictionary<T>(reader) : UseDictionary<T>(reader);

    public bool IsType(string typename) => TypeName.IsEqual(typename);
  }

  public sealed class CollectionSpInfo : ICollectionSpInfo
  {
    private readonly IEnumerable<SpInfo> SpInfos;

    public CollectionSpInfo(ICollectionMapper mappers, IConnectionManager connectionManager)
    {
      SpInfos = Read(mappers, connectionManager.App());
    }

    public ISpInfo Get(string typename, OperationType op) => SpInfos.FirstOrDefault(sp => sp.Op.IsEqual(op.ToString()) && sp.Type.IsEqual(typename));

    private IEnumerable<SpInfo> Read(ICollectionMapper mappers, string conStr)
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
      IMapper map = mappers.Get<SpProperty>();

      while (reader.Read())
      {
        var prop = map.Parse<SpProperty>(reader);
        var pars = parameters.Where(p => p.SpId == prop.Id);

        ret.Add(new SpInfo(prop, pars));
      }

      return ret;
    }

    private IEnumerable<SpParameter> ReadSpParameter(ICollectionMapper mappers, string conStr)
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

  public sealed class Context : IContext
  {
    private readonly IConnectionManager ConnectionStrings;
    private readonly ICollectionMapper Mappers;
    private readonly ICollectionSpInfo SpInfos;

    public Context(IConnectionManager conmanager, ICollectionMapper mappers, ICollectionSpInfo spinfos)
    {
      ConnectionStrings = conmanager;
      Mappers = mappers;
      SpInfos = spinfos;
    }

    public IReadOnly<T> ReferenceData<T>(int rootId) where T : new() =>
      new ReadOnly<T>(UserClaim.AppUser(ConnectionStrings, (rootId == 0) ? int.Parse(ConnectionStrings.Get("AppId")) : rootId),
                      SpInfos.Get(typeof(T).Name, OperationType.R),
                      Mappers);

    public IReadOnly<T> ReadOnly<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : new() =>
      new ReadOnly<T>(UserClaim.Get(ConnectionStrings, uc, appId),
                      SpInfos.Get(typeof(T).Name, op),
                      Mappers);

    public IWrite<T> Write<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : new() =>
      new Write<T>(UserClaim.Get(ConnectionStrings, uc, appId),
                   SpInfos.Get(typeof(T).Name, op),
                   SpInfos.Get(typeof(T).Name, OperationType.R),
                   Mappers);

    public sealed class UserClaim
    {
      public readonly int RootId;
      public readonly string ConnectionString;

      private UserClaim(IConnectionManager conManager, ClaimsPrincipal cp, int appId)
      {
        foreach (var c in cp.Claims)
        {
          switch (c.Type)
          {
            case Constant.CLIENT_ID:
              RootId = int.Parse(c.Value);
              break;

            case Constant.ROLE:
              ConnectionString = conManager.Get(c.Value);
              break;
          }
        }

        if (RootId <= 0)
          RootId = appId;
      }

      private UserClaim(string conStr, int rootId)
      {
        ConnectionString = conStr;
        RootId = rootId;
      }

      internal static UserClaim Get(IConnectionManager conmanager, ClaimsPrincipal uc, int appid = 0) => new UserClaim(conmanager, uc, appid);

      internal static UserClaim AppUser(IConnectionManager conManager, int rootId) => new UserClaim(conManager.App(), rootId);
    }
  }
}