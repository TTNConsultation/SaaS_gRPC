using System;
using System.Collections.Generic;
using System.Data;

using Microsoft.Data.SqlClient;

using Saas.Security;
using System.Threading.Tasks;

namespace Saas.Dal
{
  public enum OperationType { C, R, RR, U, D, ND }

  internal abstract class SpBase<T> : IDisposable where T : new()
  {
    private readonly SqlCommand SqlCmd;
    private readonly User UserInfo;
    private readonly SpInfo SpInformation;
    private readonly ICollectionSpToEntity Mappers;

    protected SpBase(User user, SpInfo spinfo, ICollectionSpToEntity mappers, string conStr)
    {
      UserInfo = user;
      SpInformation = spinfo;
      Mappers = mappers;
      SqlCmd = new SqlCommand(SpInformation.FullName, new SqlConnection(conStr))
      {
        CommandType = CommandType.StoredProcedure
      };
    }

    protected bool AddParameter(string name, object value)
    {
      var spParam = SpInformation.Parameter(name);

      return (spParam != null) && SqlCmd.Parameters.Add(new SqlParameter(spParam.Name, spParam.Type.ToSqlDbType())
      {
        Direction = spParam.IsOutput ? ParameterDirection.Output : ParameterDirection.Input,
        Value = value,
        Size = spParam.GetSize(value)
      }).Size > 0;
    }

    protected bool AddParameters(T obj)
    {
      if (obj == null)
        return false;

      var propInfos = obj.GetType().GetProperties();
      int nb = 0;

      for (int i = 0; i < propInfos.Length; i++)
      {
        if (AddParameter(propInfos[i].Name, propInfos[i].GetValue(obj)))
          nb++;
      }
      return nb == SpInformation.ParameterCount - 1;  //rootid
    }

    protected bool AddParameters(IDictionary<string, object> parameters)
    {
      bool ret = true;
      foreach (var p in parameters)
      {
        ret = ret && AddParameter(p.Key, p.Value);
      }
      return ret;
    }

    protected bool SetParameter(string name, object value)
    {
      var par = SpInformation.Parameter(name);
      if (par == null)
        return false;

      SqlCmd.Parameters[par.Order - 1].Value = value;
      return true;
    }

    protected bool UpdateDelete()
    {
      AddParameter(Constant.ROOT.Id(), UserInfo.RootId);
      SqlCmd.Connection.Open();

      return SqlCmd.ExecuteNonQuery() == 1;
    }

    protected int Create()
    {
      AddParameter(Constant.ROOT.Id(), UserInfo.RootId);
      SqlCmd.Connection.Open();

      return (SqlCmd.ExecuteNonQuery() == 1) ?
        int.Parse(SqlCmd.Parameters[Constant.ID].Value.ToString()) :
        -1;
    }

    public async virtual Task<IEnumerable<T>> ReadAsync()
    {
      AddParameter(Constant.ROOT.Id(), UserInfo.RootId);

      await SqlCmd.Connection.OpenAsync().ConfigureAwait(false);
      using var reader = await SqlCmd.ExecuteReaderAsync().ConfigureAwait(false);

      return await reader.ParseAsync<T>(Mappers).ConfigureAwait(false);
    }

    public virtual void Dispose()
    {
      SqlCmd?.Connection?.Close();
      SqlCmd?.Dispose();
    }
  }
}