using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;

using Constant;

namespace Protos.Dal
{
  public partial class Procedure
  {
    public readonly List<Parameter> Parameters = new List<Parameter>();

    public SqlCommand SqlCommand(string conStr) =>
      new SqlCommand(FullName, new SqlConnection(conStr)) { CommandType = CommandType.StoredProcedure };

    public Parameter Parameter(string name) => Parameters.FirstOrDefault(p => p.ParameterName.IsEqual(name.AsParameter()));

    public bool IsEqual(string spName, string operation) => this.Type.IsEqual(spName) && this.Op.IsEqual(operation);
  }

  public partial class Parameter
  {
    private int Size(object value)
    {
      if (value == null || value == DBNull.Value || string.IsNullOrEmpty(Collation))
        return MaxLength;

      var size = value.ToString().Length;

      return size <= MaxLength ? size : -1;
    }

    public string ParameterName => this.Name;

    public int ProcedureId => this.SpId;

    public SqlParameter SqlParameter(object value) =>
      new SqlParameter(Name, Type.ToSqlDbType())
      {
        Direction = IsOutput ? ParameterDirection.Output : ParameterDirection.Input,
        Value = value ?? DBNull.Value,
        Size = Size(value)
      };
  }
}