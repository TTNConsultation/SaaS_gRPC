﻿using System;
using System.Collections.Generic;
using System.Data;

using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace Dal.Sp
{
  public enum OperationType { C, R, RR, U, D, ND }

  internal abstract class Base<T> : IDisposable where T : new()
  {
    private readonly SqlCommand SqlCmd;
    private readonly User UserInfo;
    private readonly Info SpInfo;
    private readonly ICollectionMapToEntity MapToEntities;

    protected Base(User user, Info spinfo, ICollectionMapToEntity mappers, string conStr)
    {
      UserInfo = user;
      SpInfo = spinfo;
      MapToEntities = mappers;

      SqlCmd = new SqlCommand(SpInfo.FullName, new SqlConnection(conStr))
      {
        CommandType = CommandType.StoredProcedure
      };

      AddParameter(Constant.ROOT.Id(), UserInfo.RootId);
    }

    protected bool AddParameter(string key, object value)
    {
      var spParam = SpInfo.Parameter(key);

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
      return nb == SpInfo.ParameterCount - 1;  //rootid
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

    protected bool SetParameter(string key, object value)
    {
      var par = SpInfo.Parameter(key);
      if (par == null)
        return false;

      SqlCmd.Parameters[par.Order - 1].Value = value;
      return true;
    }

    protected bool Update()
    {
      SqlCmd.Connection.Open();

      return SqlCmd.ExecuteNonQuery() == 1;
    }

    protected int Create()
    {
      SqlCmd.Connection.Open();

      return (SqlCmd.ExecuteNonQuery() == 1) ?
        int.Parse(SqlCmd.Parameters[Constant.ID].Value.ToString()) :
        -1;
    }

    public IEnumerable<T> Read()
    {
      SqlCmd.Connection.Open();

      using var reader = SqlCmd.ExecuteReader();
      return reader.Parse<T>(MapToEntities);
    }

    public async virtual Task<IEnumerable<T>> ReadAsync()
    {
      await SqlCmd.Connection.OpenAsync().ConfigureAwait(false);

      using var reader = await SqlCmd.ExecuteReaderAsync().ConfigureAwait(false);
      return await reader.ParseAsync<T>(MapToEntities).ConfigureAwait(false);
    }

    public virtual void Dispose()
    {
      SqlCmd?.Connection?.Close();
      SqlCmd?.Dispose();
    }
  }
}