using Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DbContext.MsSql
{
  public partial class Procedure
  {
    public readonly List<Parameter> Parameters = new();

    public Parameter Parameter(string name) => Parameters.FirstOrDefault(p => p.Name.IsEqual(name.AsParameter()));

    public bool IsEqual(string type, string operation) => this.Type.IsEqual(type) && this.Op.IsEqual(operation);
  }

  public partial class Parameter
  {
    public int Size(object value)
    {
      if (value == null || value == DBNull.Value || string.IsNullOrEmpty(Collation))
        return MaxLength;

      var size = value.ToString().Length;

      return size <= MaxLength ? size : -1;
    }
  }
}