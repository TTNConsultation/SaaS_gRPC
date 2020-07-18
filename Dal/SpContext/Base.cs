using System;
using System.Collections.Generic;

using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Text;

namespace Dal.Sp
{
  public enum OperationType { C, R, RR, U, D, ND }

  internal abstract class Base<T> where T : new()
  {
    private readonly SqlCommand SqlCmd;
    private readonly ISpInfo SpInfo;
    private readonly IMapper Map;
    private readonly string Err;
    private readonly Context.UserClaim UserClaim;

    public bool IsReady() => string.IsNullOrEmpty(Err);

    public string Error() => Err;

    public Context.UserClaim Claim() => UserClaim;

    protected Base(Context.UserClaim claim, ISpInfo spinfo, IMapper map)
    {
      Err = new StringBuilder().Append((spinfo == null) ? "sp is null | " : null)
                               .Append((claim == null) ? "claim is null" : null)
                               .ToString();

      if (IsReady())
      {
        UserClaim = claim;
        SpInfo = spinfo;
        Map = map;
        SqlCmd = SpInfo.SqlCommand(claim.ConnectionString);
        AddParameter(Constant.ROOT.Id(), claim.RootId);
      }
    }

    protected bool AddParameter(string key, object value)
    {
      var par = SpInfo.Parameter(key)?.SqlParameter(value);

      return par?.Size >= 0 && SqlCmd.Parameters.Add(par) != null;
    }

    protected bool AddParameters(T obj)
    {
      foreach (var prop in obj.GetType().GetProperties())
      {
        if (!AddParameter(prop.Name, prop.GetValue(obj)))
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

      return reader.Parse<T>(Map);
    }

    public async Task<IEnumerable<T>> ReadAsync()
    {
      await SqlCmd.Connection.OpenAsync().ConfigureAwait(false);
      using var reader = await SqlCmd.ExecuteReaderAsync().ConfigureAwait(false);

      return await reader.ParseAsync<T>(Map).ConfigureAwait(false);
    }

    public virtual void Dispose()
    {
      SqlCmd?.Connection?.Close();
      SqlCmd?.Dispose();
    }
  }
}