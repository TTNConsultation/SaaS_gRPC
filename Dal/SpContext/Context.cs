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
    private readonly ICollectionMapToEntity Mappers;
    private readonly IEnumerable<Info> SpInfos;

    public Context(IConfiguration config, ICollectionMapToEntity mappers)
    {
      var aaf = config.GetSection(Constant.DAL);
      if (aaf == null)
        throw new NotSupportedException();

      ConStrManager = new ConnectionStringManager(aaf.GetSection(Constant.DAL_CONNECTIONSTRINGS));
      Mappers = mappers;
      SpInfos = Info.Read(mappers, ConStrManager.GetAppConnectionString());
    }

    private Info GetSpInfo<T>(RolePolicy.Role role, OperationType op)
    {
      return SpInfos.FirstOrDefault(sp => sp.OpType == op &&
                                         sp.Role.IsUnder(role) &&
                                         sp.Base.IsEqual(typeof(T).Name));
    }

    public IRonly<T> SpAppData<T>() where T : new()
    {
      var user = User.GetAppUser();
      var spInfo = GetSpInfo<T>(user.Role, OperationType.R);
      var conStr = ConStrManager.GetAppConnectionString();

      return (spInfo == null && user.IdVerified) ? null : new Ronly<T>(user, spInfo, Mappers, conStr);
    }

    public IRonly<T> SpROnly<T>(IAppData appdata, ClaimsPrincipal uc, OperationType op) where T : new()
    {
      var user = new User(uc, appdata);
      var spInfo = GetSpInfo<T>(user.Role, op);
      var conStr = ConStrManager.GetConnectionString(user.Role.ToString());

      return (spInfo == null && user.IdVerified) ? null : new Ronly<T>(user, spInfo, Mappers, conStr);
    }

    public ICrud<T> SpCrud<T>(IAppData appdata, ClaimsPrincipal uc, OperationType op) where T : new()
    {
      var user = new User(uc, appdata);
      var spInfoR = GetSpInfo<T>(user.Role, OperationType.R);
      var spInfo = GetSpInfo<T>(user.Role, op);
      var conStr = ConStrManager.GetConnectionString(user.Role.ToString());

      return (spInfo?.IsReadOnly ?? false && user.IdVerified) ? null : new Crud<T>(user, spInfo, spInfoR, Mappers, conStr);
    }
  }

  internal class ConnectionStringManager : IConnectionStringManager
  {
    private readonly Tuple<int, string, string>[] ConnectionStrings;

    public ConnectionStringManager(IConfigurationSection config)
    {
      var constrs = config.GetChildren();
      ConnectionStrings = new Tuple<int, string, string>[constrs.Count()];
      for (int i = 0; i < constrs.Count(); i++)
      {
        ConnectionStrings[i] = new Tuple<int, string, string>(i + 1, constrs.ElementAt(i).Key, constrs.ElementAt(i).Value);
      }
    }

    public string GetConnectionString(string role)
    {
      return Array.Find(ConnectionStrings, cs => cs.Item2.IsEqual(role))?.Item3;
    }

    public string GetConnectionString(int order)
    {
      return Array.Find(ConnectionStrings, cs => cs.Item1 == order)?.Item3;
    }

    public string GetAppConnectionString()
    {
      return ConnectionStrings.OrderBy(cs => cs.Item1).First().Item3;
    }
  }
}