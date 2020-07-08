using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

using Saas.Security;
using Saas.Entity.App;

namespace Saas.Dal
{
  internal sealed class SpContext : ISpContext
  {
    private readonly IConnectionStringManager ConStrManager;
    private readonly ICollectionSpToEntity Mappers;
    private readonly IEnumerable<SpInfo> SpInfos;

    public SpContext(IConfiguration config, ICollectionSpToEntity mappers)
    {
      var aaf = config.GetSection(Constant.DAL);
      if (aaf == null)
        throw new NotSupportedException();

      ConStrManager = new ConnectionStringManager(aaf.GetSection(Constant.DAL_CONNECTIONSTRINGS));
      Mappers = mappers;
      SpInfos = SpInfo.Read(mappers, ConStrManager.GetAppConnectionString());
    }

    private SpInfo GetSpInfo<T>(RolePolicy.Role role, OperationType op)
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

      return (spInfo == null && user.IdVerified) ? null : new SpRonly<T>(user, spInfo, Mappers, conStr);
    }

    public IRonly<T> SpROnly<T>(AppData appdata, ClaimsPrincipal uc, OperationType op) where T : new()
    {
      var user = new User(uc, appdata);
      var spInfo = GetSpInfo<T>(user.Role, op);
      var conStr = ConStrManager.GetConnectionString(user.Role.ToString());

      return (spInfo == null && user.IdVerified) ? null : new SpRonly<T>(user, spInfo, Mappers, conStr);
    }

    public ICrud<T> SpCrud<T>(AppData appdata, ClaimsPrincipal uc, OperationType op) where T : new()
    {
      var user = new User(uc, appdata);
      var spInfoR = GetSpInfo<T>(user.Role, OperationType.R);
      var spInfo = GetSpInfo<T>(user.Role, op);
      var conStr = ConStrManager.GetConnectionString(user.Role.ToString());

      return (spInfo?.IsReadOnly ?? false && user.IdVerified) ? null : new SpCrud<T>(user, spInfo, spInfoR, Mappers, conStr);
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