using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dal.Sp
{
  public interface ISpInfo
  {
    SqlCommand SqlCommand(string conStr);

    IParameter Parameter(string name);
  }

  public interface IParameter
  {
    int Size(object value);

    SqlParameter SqlParameter(object value);
  }

  public sealed class SpInfo : ISpInfo
  {
    private readonly SpProperty property;
    private readonly IEnumerable<SpParameter> parameters;

    internal string Op => property.Op;
    internal string Type => property.Type;

    public SqlCommand SqlCommand(string conStr)
    {
      var cmd = new SqlCommand(property.FullName, new SqlConnection(conStr))
      {
        CommandType = CommandType.StoredProcedure,
      };
      //cmd.Parameters.AddRange(parameters.Select(p => p.SqlParameter()).ToArray());

      return cmd;
    }

    public IParameter Parameter(string name) => parameters.FirstOrDefault(p => p.Name.IsEqual(name.AsParameter()));

    internal SpInfo(SpProperty prop, IEnumerable<SpParameter> pars)
    {
      property = prop;
      parameters = pars ?? new HashSet<SpParameter>();
    }
  }

  internal sealed class SpProperty
  {
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Schema { get; set; }
    public string Type { get; set; }
    public string Op { get; set; }
  }

  internal sealed class SpParameter : IParameter
  {
    public string SpName { get; set; }
    public int SpId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int MaxLength { get; set; }
    public int Precision { get; set; }
    public int Scale { get; set; }
    public int Order { get; set; }
    public bool IsOutput { get; set; }
    public string Collation { get; set; }

    public int Size(object value)
    {
      if (value == null || value == DBNull.Value)
        return 0;

      int size;
      return (string.IsNullOrEmpty(Collation))
              ? MaxLength
              : (size = value.ToString().Length) <= Precision ? size : -1;
    }

    public SqlParameter SqlParameter(object value) =>
      new SqlParameter(Name, Type.ToSqlDbType())
      {
        Direction = IsOutput ? ParameterDirection.Output : ParameterDirection.Input,
        Value = value ?? DBNull.Value,
        Size = Size(value)
      };
  }
}