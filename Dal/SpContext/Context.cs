using System;
using System.Collections.Generic;
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

  public interface ICollectionMap
  {
    public IMap Get<T>();

    T Add<T>(SqlDataReader reader, out IMap mapper) where T : new();
  }

  public interface IMap
  {
    bool IsType(string type);

    T Build<T>(SqlDataReader reader) where T : new();

    T Parse<T>(SqlDataReader reader) where T : new();
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
      ConnectionStrings = config.GetSection(Constant.CONNECTIONSTRINGS)?.GetChildren()?.ToDictionary(s => s.Key, s => s.Value);
    }

    public string Get(string schema) => ConnectionStrings.FirstOrDefault(s => s.Key.IsEqual(schema)).Value;

    public string App() => Get(Constant.APP);
  }

  public sealed class CollectionMapper : ICollectionMap
  {
    private readonly HashSet<IMap> mappers = new HashSet<IMap>();

    public IMap Get<T>() => mappers.FirstOrDefault(sp => sp.IsType(typeof(T).Name));

    public T Add<T>(SqlDataReader reader, out IMap map) where T : new()
    {
      map = new Mapper(typeof(T).Name);
      mappers.Add(map);

      return map.Build<T>(reader);
    }
  }

  public sealed class Mapper : IMap
  {
    private readonly string Name;
    private IDictionary<int, PropertyInfo> Map;

    internal Mapper(string name)
    {
      Name = name;
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

    public bool IsType(string name)
    {
      return Name.IsEqual(name);
    }
  }

  public sealed class Context : IContext
  {
    private readonly IConnectionManager ConnectionStrings;
    private readonly ICollectionMap Mappers;
    private readonly SpInfoManager SpInfos;

    public Context(IConnectionManager conmanager, ICollectionMap mappers)
    {
      ConnectionStrings = conmanager;
      Mappers = mappers;
      SpInfos = new SpInfoManager(Mappers, ConnectionStrings.App());
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