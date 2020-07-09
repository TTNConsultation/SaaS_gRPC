using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace Dal.Sp
{
  internal sealed class SpInfo
  {
    private readonly SpProperty property;
    private readonly IEnumerable<SpParameter> parameters;

    internal readonly int ParameterCount;
    internal readonly OperationType Op;
    internal readonly string Name;
    internal readonly string FullName;
    internal readonly string ErrorMessage = string.Empty;

    internal SpParameter Parameter(string name) => parameters.FirstOrDefault(p => p.Name.IsEqual(name.AsParameter()));

    internal SpInfo(SpProperty prop, IEnumerable<SpParameter> pars)
    {
      property = prop;
      Name = property.Base;
      FullName = property.Schema.DotAnd(property.Name);

      Op = property.Op switch
      {
        "R" => OperationType.R,
        "RR" => OperationType.RR,
        "C" => OperationType.C,
        "U" => OperationType.U,
        "D" => OperationType.D,
        _ => OperationType.ND,
      };

      parameters = pars ?? new HashSet<SpParameter>();
      ParameterCount = parameters.Count();
    }
  }

  internal sealed class SpProperty
  {
    public int Id { get; set; }
    public string Schema { get; set; }
    public string Name { get; set; }
    public string Base { get; set; }
    public string Op { get; set; }
  }

  internal sealed class SpParameter
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

    internal int GetSize(object value)
    {
      return (string.IsNullOrEmpty(Collation)) ? MaxLength
                                               : ((value.ToString().Length <= Precision) ? value.ToString().Length : -1);
    }
  }
}