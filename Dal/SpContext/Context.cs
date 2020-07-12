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
    IMapper Get<T>();

    T Add<T>(SqlDataReader reader, out IMapper mapper) where T : new();
  }

  public interface IMapper
  {
    string TypeName();

    bool IsType(string type);

    T Build<T>(SqlDataReader reader) where T : new();

    T Parse<T>(SqlDataReader reader) where T : new();
  }

  public interface ICollectionSpInfo
  {
    ISpInfo Get(string type, OperationType op);
  }

  public interface IContext
  {
    IReadOnly<T> ReferenceData<T>(int appId = 0) where T : new();

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

    private IMapper Add(IMapper map) => mappers.Add(map) ? map : mappers.First(m => m.IsType(map.TypeName()));

    public IMapper Get<T>() => mappers.FirstOrDefault(sp => sp.IsType(typeof(T).Name));

    public T Add<T>(SqlDataReader reader, out IMapper map) where T : new() =>
      Add(map = new Mapper(typeof(T).Name)).Build<T>(reader);
  }

  public sealed class Mapper : IMapper
  {
    private IDictionary<int, PropertyInfo> Map;
    private readonly string Type;

    internal Mapper(string type)
    {
      Type = type;
    }

    public T Build<T>(SqlDataReader reader) where T : new()
    {
      Map = new Dictionary<int, PropertyInfo>();
      var propInfos = typeof(T).GetProperties();
      var ret = new T();

      for (int i = 0; i < reader.FieldCount; i++)
      {
        var propInfo = propInfos?.FirstOrDefault(pi => pi.Name.IsEqual(reader.GetName(i)));
        if (propInfo != null)
        {
          propInfo.SetValue(ret, Convert.ChangeType(reader[i], propInfo.PropertyType));
          Map.Add(new KeyValuePair<int, PropertyInfo>(i, propInfo));
        }
      }

      return ret;
    }

    public T Parse<T>(SqlDataReader reader) where T : new()
    {
      var ret = new T();
      foreach (var m in Map)
      {
        m.Value.SetValue(ret, Convert.ChangeType(reader[m.Key], m.Value.PropertyType));
      }
      return ret;
    }

    public string TypeName() => Type;

    public bool IsType(string name) => Type.IsEqual(name);
  }

  public sealed class CollectionSpInfo : ICollectionSpInfo
  {
    private readonly IEnumerable<SpInfo> SpInfos;

    public CollectionSpInfo(ICollectionMapper mappers, IConnectionManager connectionManager)
    {
      SpInfos = Read(mappers, connectionManager.App());
    }

    public ISpInfo Get(string type, OperationType op) => SpInfos.FirstOrDefault(sp => sp.Op.IsEqual(op.ToString()) && sp.Type.IsEqual(type));

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
      IMapper map = null;

      while (reader.Read())
      {
        var prop = (map == null) ? mappers.Add<SpProperty>(reader, out map) : map.Parse<SpProperty>(reader);
        var pars = parameters.Where(p => p.SpId == prop.Id).OrderBy(p => p.Order);

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

    public IReadOnly<T> ReferenceData<T>(int appId = 0) where T : new() =>
      new ReadOnly<T>(UserClaim.AppUser(ConnectionStrings, appId),
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

    internal sealed class UserClaim
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

      private UserClaim(string conStr, int appId)
      {
        ConnectionString = conStr;
        RootId = appId;
      }

      internal static UserClaim Get(IConnectionManager conmanager, ClaimsPrincipal uc, int appid) => new UserClaim(conmanager, uc, appid);

      internal static UserClaim AppUser(IConnectionManager conManager, int appId) => new UserClaim(conManager.App(), appId);
    }
  }
}