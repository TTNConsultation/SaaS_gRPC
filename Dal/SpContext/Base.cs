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
    private readonly SpInfo SpInfo;
    private readonly int RootId;
    private readonly ISpMappers MapToEntities;

    public bool IsInit() => SqlCmd != null;

    protected Base(ConnectionStringManager.User user, SpInfo spinfo, ISpMappers mappers)
    {
      RootId = user.RootId;
      SpInfo = spinfo;
      MapToEntities = mappers;

      SqlCmd = (spinfo == null || string.IsNullOrEmpty(user.ConnectionStr)) ? null : new SqlCommand(spinfo.FullName, new SqlConnection(user.ConnectionStr))
      {
        CommandType = CommandType.StoredProcedure
      };
    }

    protected bool SetParameter(string key, object value)
    {
      var spParam = SpInfo.Parameter(key);

      return (spParam != null) && SqlCmd.Parameters.Add(new SqlParameter(spParam.Name, spParam.Type.ToSqlDbType())
      {
        Direction = spParam.IsOutput ? ParameterDirection.Output : ParameterDirection.Input,
        Value = value,
        Size = spParam.GetSize(value)
      }).Size > 0;
    }

    protected bool SetParameter(T obj)
    {
      var propInfos = obj.GetType().GetProperties();
      foreach (var prop in propInfos)
      {
        SetParameter(prop.Name, prop.GetValue(obj));
      }

      return propInfos.Length == SpInfo.ParameterCount - 1;  //rootid
    }

    protected bool SetParameter(IDictionary<string, object> parameters)
    {
      bool ret = true;
      foreach (var p in parameters)
      {
        ret = ret && SetParameter(p.Key, p.Value);
      }
      return ret;
    }

    protected bool SetParameterValue(string key, object value)
    {
      var par = SpInfo.Parameter(key);
      if (par == null)
        return false;

      SqlCmd.Parameters[par.Order - 1].Value = value;

      return true;
    }

    private void AddRootAndConnect()
    {
      SetParameter(Constant.ROOT.Id(), RootId);
      SqlCmd.Connection.Open();
    }

    protected bool Update()
    {
      AddRootAndConnect();

      return SqlCmd.ExecuteNonQuery() == 1;
    }

    protected int Create()
    {
      AddRootAndConnect();

      return (SqlCmd.ExecuteNonQuery() == 1) ?
        int.Parse(SqlCmd.Parameters[Constant.ID].Value.ToString()) :
        -1;
    }

    public IEnumerable<T> Read()
    {
      AddRootAndConnect();
      using var reader = SqlCmd.ExecuteReader();
      return reader.Parse<T>(MapToEntities);
    }

    public async virtual Task<IEnumerable<T>> ReadAsync()
    {
      SetParameter(Constant.ROOT.Id(), RootId);

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