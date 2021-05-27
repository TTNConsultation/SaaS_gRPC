using Utility;
using DbContext.Interfaces;
using Google.Protobuf;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Google.Protobuf.WellKnownTypes;

namespace DbContext.MsSql.Command
{
  internal abstract class Base
  {
    private readonly Procedure _procedure;
    protected readonly IDbCommand dbCmd;

    public bool IsValid => dbCmd != null;

    protected Base(Procedure procedure, string conStr)
    {
      _procedure = procedure;
      dbCmd = (_procedure == null) ? null
                                   : new SqlCommand(_procedure.FullName, new SqlConnection(conStr)) { CommandType = CommandType.StoredProcedure };
    }

    public bool AddParameter(string key, object value)
    {
      var par = _procedure.Parameter(key);
      return (par != null) &&
              dbCmd.Parameters.Add(new SqlParameter(par.Name, par.Type.ToSqlDbType())
              {
                Direction = par.IsOutput ? ParameterDirection.Output : ParameterDirection.Input,
                Value = value?.GetType() == typeof(Timestamp) ? ((Timestamp)value).ToDateTime() : value ?? DBNull.Value,
                Size = par.Size(value)
              }) >= 0;
    }

    public bool AddParameter(IMessage obj) =>
      obj.Descriptor.Fields.InDeclarationOrder().Where(fd => AddParameter(fd.Name, fd.Accessor.GetValue(obj))).Count() == _procedure.Parameters.Count;

    public bool AddParameter(IDictionary<string, object> par) =>
      par.Where(p => AddParameter(p.Key, p.Value)).Count() == par.Count;
  }

  internal sealed class Writer<T> : Base, IWriter<T> where T : IMessage<T>, new()
  {
    public IReader<T> Reader { get; }

    public Writer(Mapper map, Procedure procedure, Procedure rprocedure, string conStr) : base(procedure, conStr)
    {
      Reader = new Reader<T>(map, rprocedure, conStr);
    }

    public int ExecuteNonQuery()
    {
      using var sqlCon = dbCmd.Connection;
      sqlCon.Open();

      return dbCmd.ExecuteNonQuery();
    }

    public object ExecuteScalar()
    {
      using var sqlCon = dbCmd.Connection;
      sqlCon.Open();

      return dbCmd.ExecuteScalar();
    }
  }

  internal sealed class Reader<T> : Base, IReader<T> where T : IMessage<T>, new()
  {
    public readonly Mapper _map;

    public Reader(Mapper map, Procedure procedure, string conStr) : base(procedure, conStr)
    {
      _map = map;
    }

    public ICollection<T> ExecuteReader()
    {
      var ret = new HashSet<T>();

      using var sqlcon = dbCmd.Connection;
      sqlcon.Open();

      using var reader = dbCmd.ExecuteReader();

      while (reader.Read())
      {
        ret.Add(_map.Parse<T>(reader));
      }

      return ret;
    }

    public async Task<ICollection<T>> ExecuteAsync()
    {
      var ret = new HashSet<T>();

      using var sqlcon = dbCmd.Connection;
      sqlcon.Open();

      using var reader = await ((SqlCommand)dbCmd).ExecuteReaderAsync();

      while (await reader.ReadAsync())
      {
        ret.Add(_map.Parse<T>(reader));
      }

      return ret;
    }
  }
}