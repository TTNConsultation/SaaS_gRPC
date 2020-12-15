using System;
using System.Collections.Generic;

using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Text;
using Google.Protobuf;

using StoreProcedure.Interface;

namespace StoreProcedure.Command
{
  internal abstract class Base<T> where T : IMessage, new()
  {
    private readonly SqlCommand SqlCmd;
    private readonly IStoreProcedure SpProperty;
    private readonly ICollectionMapper FieldMaps;

    public string Error { get; }
    public int RootId { get; }
    public bool IsReady => string.IsNullOrEmpty(Error);

    protected Base(UserClaim claim, IStoreProcedure sp, ICollectionMapper reflectionMaps)
    {
      Error = new StringBuilder().Append((sp == null) ? "store procedure not found | " : null)
                               .Append((claim == null) ? "invalid claim" : null)
                               .ToString();

      if (IsReady)
      {
        RootId = claim.RootId;
        SpProperty = sp;
        FieldMaps = reflectionMaps;
        SqlCmd = SpProperty.SqlCommand(claim.ConnectionString);
        AddParameter(Constant.ROOT.Id(), RootId);
      }
    }

    protected bool AddParameter(string key, object value)
    {
      var par = SpProperty.Parameter(key)?.SqlParameter(value);

      return (par != null) && SqlCmd.Parameters.Add(par).Size >= 0;
    }

    protected bool AddParameters(T obj)
    {
      foreach (var fd in obj.Descriptor.Fields.InDeclarationOrder())
      {
        if (!AddParameter(fd.Name, fd.Accessor.GetValue(obj)))
          return false;
      }

      return true;
    }

    protected bool AddParameters(IDictionary<string, object> parameters)
    {
      foreach (var p in parameters)
      {
        if (!AddParameter(p.Key, p.Value))
          return false;
      }
      return true;
    }

    protected bool SetParameter(string key, object value)
    {
      int index = SqlCmd.Parameters.IndexOf(key);
      return (index >= 0) && (SqlCmd.Parameters[index].Value = (value ?? DBNull.Value)) != null;
    }

    protected bool Update()
    {
      SqlCmd.Connection.Open();
      return SqlCmd.ExecuteNonQuery() == 1;
    }

    protected int Create()
    {
      SqlCmd.Connection.Open();

      return (SqlCmd.ExecuteNonQuery() == 1)
        ? int.Parse(SqlCmd.Parameters[Constant.ID].Value.ToString())
        : -1;
    }

    public IEnumerable<T> Read()
    {
      SqlCmd.Connection.Open();
      using var reader = SqlCmd.ExecuteReader();

      return reader.Parse<T>(FieldMaps);
    }

    public async Task<IEnumerable<T>> ReadAsync()
    {
      await SqlCmd.Connection.OpenAsync().ConfigureAwait(false);
      using var reader = await SqlCmd.ExecuteReaderAsync().ConfigureAwait(false);

      return await reader.ParseAsync<T>(FieldMaps).ConfigureAwait(false);
    }

    public virtual void Dispose()
    {
      SqlCmd?.Connection?.Close();
      SqlCmd?.Dispose();
    }
  }

  internal sealed class ExecuteNonQuery<T> : Base<T>, IExecuteNonQuery<T> where T : IMessage, new()
  {
    private readonly IExecuteReader<T> SpRO;

    public ExecuteNonQuery(UserClaim claim, IStoreProcedure sp, IStoreProcedure spReadOnly, ICollectionMapper reflectionMaps) : base(claim, sp, reflectionMaps)
    {
      SpRO = (spReadOnly == null) ? null : new ExecuteReader<T>(claim, spReadOnly, reflectionMaps);
    }

    public int Create(T obj) => AddParameters(obj) ? Create() : -1;

    public bool Update(T obj) => AddParameters(obj) && Update();

    public bool UpdateState(int id, int stateId) => AddParameters(SpRO.Read(id)) && AddParameter(Constant.STATE.Id(), stateId) && Update();

    public bool Delete(int id) => AddParameter(Constant.ID, id) && Update();

    public override void Dispose()
    {
      SpRO?.Dispose();
      base.Dispose();
    }
  }

  internal sealed class ExecuteReader<T> : Base<T>, IExecuteReader<T> where T : IMessage, new()
  {
    public ExecuteReader(UserClaim claim, IStoreProcedure sp, ICollectionMapper reflectionMaps) : base(claim, sp, reflectionMaps)
    {
    }

    public IEnumerable<T> Read(string key, object value) => AddParameter(key, value) ? Read() : null;

    public IEnumerable<T> Read(IDictionary<string, object> parameters) => AddParameters(parameters) ? Read() : null;

    public IEnumerable<T> ReadRange(string key, string values, char separator) => AddParameter(key, values) && AddParameter(Constant.SEPARATOR, separator) ? Read() : null;

    public async Task<IEnumerable<T>> ReadAsync(string key, object value) => AddParameter(key, value) ? await ReadAsync().ConfigureAwait(false) : null;

    public async Task<IEnumerable<T>> ReadAsync(IDictionary<string, object> parameters) => AddParameters(parameters) ? await ReadAsync().ConfigureAwait(false) : null;

    public async Task<IEnumerable<T>> ReadRangeAsync(string key, string values, char separator) => AddParameter(key, values) && AddParameter(Constant.SEPARATOR, separator) ? await ReadAsync().ConfigureAwait(false) : null;
  }
}