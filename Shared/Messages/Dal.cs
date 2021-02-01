using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Protos.Shared.Interfaces;
using Microsoft.Data.SqlClient;

namespace Protos.Shared.Dal
{
  public partial class Procedure : IProcedure
  {
    public IEnumerable<IParameter> Parameters { get; set; }

    public SqlCommand SqlCommand(string conStr) =>
      new SqlCommand(FullName, new SqlConnection(conStr)) { CommandType = CommandType.StoredProcedure };

    public IParameter Parameter(string name) => Parameters.FirstOrDefault(p => p.ParameterName.IsEqual(name.AsParameter()));

    public bool IsEqual(string spName, OperationType op) => this.Type.IsEqual(spName) && Op.IsEqual(op.ToString());
  }

  public partial class Parameter : IParameter
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