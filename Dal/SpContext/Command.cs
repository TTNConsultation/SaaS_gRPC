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
    private readonly IStoreProcedure _sp;
    protected readonly SqlCommand sqlCmd;

    public string Error { get; }
    public int RootId { get; }
    public bool IsReady => string.IsNullOrEmpty(Error);

    protected Base(Security security, IStoreProcedure sp)
    {
      Error = new StringBuilder().Append((sp == null) ? "store procedure not found | " : null)
                                 .Append((security == null) ? "invalid claim" : null)
                                 .ToString();

      if (IsReady)
      {
        _sp = sp;
        sqlCmd = _sp.SqlCommand(security.ConnectionString);
        RootId = security.RootId;

        AddParameter(Constant.ROOT.AsId(), RootId);
      }
    }

    protected bool AddParameter(string key, object value)
    {
      var par = _sp.Parameter(key)?.SqlParameter(value);

      return (par != null) && sqlCmd.Parameters.Add(par).Size >= 0;
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
      int index = sqlCmd.Parameters.IndexOf(key);
      return (index >= 0) && (sqlCmd.Parameters[index].Value = (value ?? DBNull.Value)) != null;
    }

    public virtual void Dispose()
    {
      sqlCmd?.Connection?.Close();
      sqlCmd?.Dispose();
    }
  }

  internal sealed class ExecuteNonQuery<T> : Base<T>, IExecuteNonQuery<T> where T : IMessage, new()
  {
    private readonly IExecuteReader<T> SpRO;

    public ExecuteNonQuery(Security claim, IStoreProcedure sp, IStoreProcedure spReadOnly, IMapper map) : base(claim, sp)
    {
      SpRO = (spReadOnly == null) ? null : new ExecuteReader<T>(claim, spReadOnly, map);
    }

    private bool Update()
    {
      sqlCmd.Connection.Open();
      return sqlCmd.ExecuteNonQuery() == 1;
    }

    private int Create()
    {
      sqlCmd.Connection.Open();

      return (sqlCmd.ExecuteNonQuery() == 1)
        ? int.Parse(sqlCmd.Parameters[Constant.ID].Value.ToString())
        : -1;
    }

    public int Create(T obj) => AddParameters(obj) ? Create() : -1;

    public bool Update(T obj) => AddParameters(obj) && Update();

    public bool UpdateState(int id, int stateId) => AddParameters(SpRO.Read(id)) && AddParameter(Constant.STATE.AsId(), stateId) && Update();

    public bool Delete(int id) => AddParameter(Constant.ID, id) && Update();

    public override void Dispose()
    {
      SpRO?.Dispose();
      base.Dispose();
    }
  }

  internal sealed class ExecuteReader<T> : Base<T>, IExecuteReader<T> where T : IMessage, new()
  {
    private readonly IMapper _map;

    public ExecuteReader(Security claim, IStoreProcedure sp, IMapper map) : base(claim, sp)
    {
      _map = map;
    }

    public ICollection<T> Read()
    {
      sqlCmd.Connection.Open();
      using var reader = sqlCmd.ExecuteReader();

      return reader.Parse<T>(_map);
    }

    public async Task<ICollection<T>> ReadAsync()
    {
      await sqlCmd.Connection.OpenAsync().ConfigureAwait(false);
      using var reader = await sqlCmd.ExecuteReaderAsync().ConfigureAwait(false);

      return await reader.ParseAsync<T>(_map).ConfigureAwait(false);
    }

    public ICollection<T> Read(string key, object value) => AddParameter(key, value) ? Read() : null;

    public ICollection<T> Read(IDictionary<string, object> parameters) => AddParameters(parameters) ? Read() : null;

    public ICollection<T> ReadRange(string key, string values, char separator) => AddParameter(key, values) && AddParameter(Constant.SEPARATOR, separator) ? Read() : null;

    public async Task<ICollection<T>> ReadAsync(string key, object value) => AddParameter(key, value) ? await ReadAsync().ConfigureAwait(false) : null;

    public async Task<ICollection<T>> ReadAsync(IDictionary<string, object> parameters) => AddParameters(parameters) ? await ReadAsync().ConfigureAwait(false) : null;

    public async Task<ICollection<T>> ReadRangeAsync(string key, string values, char separator) => AddParameter(key, values) && AddParameter(Constant.SEPARATOR, separator) ? await ReadAsync().ConfigureAwait(false) : null;
  }
}