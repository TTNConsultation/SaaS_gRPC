using Constant;
using DbContext.Interfaces;
using Google.Protobuf;
using Microsoft.Data.SqlClient;
using Protos.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbContext.MsSql.Command
{
  internal abstract class Base
  {
    private readonly Procedure _procedure;
    protected readonly SqlCommand sqlCmd;

    public string Error { get; }
    public int RootId { get; }
    public bool IsReady => string.IsNullOrEmpty(Error);

    protected Base(Security security, Procedure procedure)
    {
      Error = (procedure == null) ? StrVal.PROCEDURENOTFOUND
                                  : (security == null || string.IsNullOrEmpty(security.ConnectionString))
                                  ? StrVal.INVALIDCLAIM
                                  : string.Empty;

      if (IsReady)
      {
        _procedure = procedure;
        sqlCmd = _procedure.SqlCommand(security.ConnectionString);
        RootId = security.RootId;
        AddParameter(StrVal.ROOT.AndId(), RootId);
      }
    }

    public bool AddParameter(string key, object value)
    {
      var par = _procedure.Parameter(key)?.SqlParameter(value);
      return (par != null) && sqlCmd.Parameters.Add(par).Size >= 0;
    }

    protected bool SetParameter(string key, object value)
    {
      int index = sqlCmd.Parameters.IndexOf(key);
      return (index >= 0) && (sqlCmd.Parameters[index].Value = (value ?? DBNull.Value)) != null;
    }

    public virtual void Dispose()
    {
      sqlCmd?.Connection?.Close();
      sqlCmd?.Connection?.Dispose();
      sqlCmd?.Dispose();
    }
  }

  internal sealed class ExecuteNonQuery<T> : Base, IExecuteNonQuery<T> where T : IMessage<T>, new()
  {
    public IExecuteReader<T> Reader { get; }

    public ExecuteNonQuery(Security claim, Procedure procedure, Procedure rprocedure, Mapper map) : base(claim, procedure)
    {
      Reader = new ExecuteReader<T>(claim, rprocedure, map);
    }

    public bool Update()
    {
      sqlCmd.Connection.Open();
      return sqlCmd.ExecuteNonQuery() == 1;
    }

    public int Create()
    {
      sqlCmd.Connection.Open();

      return (sqlCmd.ExecuteNonQuery() == 1)
        ? int.Parse(sqlCmd.Parameters[StrVal.ID].Value.ToString())
        : throw new Exception(StrVal.OPERATIONFAILED);
    }

    public override void Dispose()
    {
      Reader?.Dispose();
      base.Dispose();
    }
  }

  internal sealed class ExecuteReader<T> : Base, IExecuteReader<T> where T : IMessage<T>, new()
  {
    private readonly Mapper _map;

    public ExecuteReader(Security claim, Procedure procedure, Mapper map) : base(claim, procedure)
    {
      _map = map;
    }

    public ICollection<T> Read()
    {
      var ret = new HashSet<T>();

      sqlCmd.Connection.Open();
      using var reader = sqlCmd.ExecuteReader();

      while (reader.Read())
      {
        ret.Add(_map.Parse<T>(reader));
      }

      return ret;
    }

    public async Task<ICollection<T>> ReadAsync()
    {
      var ret = new HashSet<T>();

      await sqlCmd.Connection.OpenAsync().ConfigureAwait(false);
      using var reader = await sqlCmd.ExecuteReaderAsync().ConfigureAwait(false);

      while (await reader.ReadAsync().ConfigureAwait(false))
      {
        ret.Add(_map.Parse<T>(reader));
      }

      return ret;
    }
  }
}