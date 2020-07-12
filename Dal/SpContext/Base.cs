using System;
using System.Collections.Generic;
using System.Data;

using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Text;

namespace Dal.Sp
{
  public enum OperationType { C, R, RR, U, D, ND }

  internal abstract class Base<T> : IDisposable where T : new()
  {
    private readonly SqlCommand SqlCmd;
    private readonly SpInfo SpInfo;
    private readonly ICollectionMap Mappers;
    private readonly string Err;

    public bool IsReady() => string.IsNullOrEmpty(Err);

    public string Error() => Err;

    protected Base(Context.UserClaim claim, SpInfo sp, ICollectionMap mappers)
    {
      Err = new StringBuilder().Append((sp == null) ? "sp is null | " : null)
                               .Append((claim == null) ? "claim is null" : null)
                               .ToString();

      if (IsReady())
      {
        SpInfo = sp;
        Mappers = mappers;
        SqlCmd = new SqlCommand(sp.StoreProcName, new SqlConnection(claim.ConnectionString))
        {
          CommandType = CommandType.StoredProcedure
        };
        SetParameter(Constant.ROOT.Id(), claim.RootId);
      }
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
      foreach (var p in parameters)
      {
        if (!SetParameter(p.Key, p.Value))
          return false;
      }
      return true;
    }

    protected bool SetParameterValue(string key, object value)
    {
      var par = SpInfo.Parameter(key);
      return par != null && (SqlCmd.Parameters[par.Order - 1].Value = value) != null;
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

      return reader.Parse<T>(Mappers);
    }

    public async Task<IEnumerable<T>> ReadAsync()
    {
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