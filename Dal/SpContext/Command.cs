using System;
using System.Collections.Generic;

using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Text;
using Google.Protobuf;

using StoreProcedure.Interface;

namespace StoreProcedure.Command
{
  internal abstract class Base
  {
    private readonly IProcedure _sp;
    protected readonly SqlCommand SqlCmd;

    public string Error { get; }
    public int RootId { get; }
    public bool IsReady => string.IsNullOrEmpty(Error);

    protected Base(Security security, IProcedure sp)
    {
      Error = new StringBuilder().Append((sp == null) ? "procedure not found | " : null)
                                 .Append((security == null) ? "invalid claim" : null)
                                 .ToString();

      if (IsReady)
      {
        _sp = sp;
        SqlCmd = _sp.SqlCommand(security.ConnectionString);
        RootId = security.RootId;

        AddParameter(Constant.ROOT.AsId(), RootId);
      }
    }

    protected bool AddParameter(string key, object value)
    {
      var par = _sp.Parameter(key)?.SqlParameter(value);

      return (par != null) && SqlCmd.Parameters.Add(par).Size >= 0;
    }

    protected bool AddParameter(IMessage obj)
    {
      foreach (var fd in obj.Descriptor.Fields.InDeclarationOrder())
      {
        if (!AddParameter(fd.Name, fd.Accessor.GetValue(obj)))
          return false;
      }

      return true;
    }

    protected bool AddParameter(IDictionary<string, object> parameters)
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

    public virtual void Dispose()
    {
      SqlCmd?.Connection?.Close();
      SqlCmd?.Connection?.Dispose();
      SqlCmd?.Dispose();
    }
  }

  internal sealed class ExecuteNonQuery<T> : Base, IExecuteNonQuery<T> where T : IMessage<T>, new()
  {
    private readonly IExecuteReader<T> _reader;

    public ExecuteNonQuery(Security claim, IProcedure sp, IProcedure spR, IMapper map) : base(claim, sp)
    {
      _reader = (spR == null) ? null : new ExecuteReader<T>(claim, spR, map);
    }

    private bool Update()
    {
      SqlCmd.Connection.Open();
      return SqlCmd.ExecuteNonQuery() == 1;
    }

    private int Create()
    {
      SqlCmd.Connection.Open();

      return (SqlCmd.ExecuteNonQuery() == 1)
        ? int.Parse(SqlCmd.Parameters[Constant.ID].Value.ToString())
        : -1;
    }

    public int Create(T obj) => AddParameter(obj) ? Create() : -1;

    public bool Update(T obj) => AddParameter(obj) && Update();

    public bool UpdateState(int id, int stateId) => AddParameter(_reader.Read(id)) && AddParameter(Constant.STATE.AsId(), stateId) && Update();

    public bool Delete(int id) => AddParameter(Constant.ID, id) && Update();

    public override void Dispose()
    {
      _reader?.Dispose();
      base.Dispose();
    }
  }

  internal sealed class ExecuteReader<T> : Base, IExecuteReader<T> where T : IMessage<T>, new()
  {
    private readonly IMapper _map;

    public ExecuteReader(Security claim, IProcedure sp, IMapper map) : base(claim, sp)
    {
      _map = map;
    }

    public ICollection<T> Read()
    {
      SqlCmd.Connection.Open();
      using var reader = SqlCmd.ExecuteReader();

      return reader.Parse<T>(_map);
    }

    public async Task<ICollection<T>> ReadAsync()
    {
      await SqlCmd.Connection.OpenAsync().ConfigureAwait(false);
      using var reader = await SqlCmd.ExecuteReaderAsync().ConfigureAwait(false);

      return await reader.ParseAsync<T>(_map).ConfigureAwait(false);
    }

    public ICollection<T> Read(string key, object value) => AddParameter(key, value) ? Read() : null;

    public ICollection<T> Read(IDictionary<string, object> parameters) => AddParameter(parameters) ? Read() : null;

    public ICollection<T> ReadRange(string key, string values, char separator) => AddParameter(key, values) && AddParameter(Constant.SEPARATOR, separator) ? Read() : null;

    public async Task<ICollection<T>> ReadAsync(string key, object value) => AddParameter(key, value) ? await ReadAsync().ConfigureAwait(false) : null;

    public async Task<ICollection<T>> ReadAsync(IDictionary<string, object> parameters) => AddParameter(parameters) ? await ReadAsync().ConfigureAwait(false) : null;

    public async Task<ICollection<T>> ReadRangeAsync(string key, string values, char separator) => AddParameter(key, values) && AddParameter(Constant.SEPARATOR, separator) ? await ReadAsync().ConfigureAwait(false) : null;
  }
}