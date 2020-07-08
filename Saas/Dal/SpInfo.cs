using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Microsoft.Data.SqlClient;
using Saas.Security;

namespace Saas.Dal
{
  internal sealed class SpInfo
  {
    private readonly SpProperty property;
    private readonly IEnumerable<SpParameter> parameters;

    internal int ParameterCount => parameters.Count();

    internal SpParameter Parameter(string name) => parameters.FirstOrDefault(p => p.Name.IsEqual(name.AsParameter()));

    internal OperationType OpType => property.Op switch
    {
      "R" => OperationType.R,
      "RR" => OperationType.RR,
      "C" => OperationType.C,
      "U" => OperationType.U,
      "D" => OperationType.D,
      _ => OperationType.ND,
    };

    internal RolePolicy.Role Role => RolePolicy.GetRole(property.Schema);
    internal string Base => property.Base;
    internal string FullName => property.Schema.DotAnd(property.Name);
    internal bool IsReadOnly => OpType == OperationType.R || OpType == OperationType.RR;

    internal SpInfo(SpProperty prop, IEnumerable<SpParameter> pars)
    {
      property = prop;
      parameters = pars ?? new HashSet<SpParameter>();
    }

    private static IEnumerable<SpParameter> ReadSpParameter(ICollectionSpToEntity mappers, string conStr)
    {
      using var sqlcmd = new SqlCommand(Constant.APP.DotAnd(Constant.TypeName<SpParameter>().UnderscoreAnd(Constant.READ)), new SqlConnection(conStr))
      {
        CommandType = CommandType.StoredProcedure
      };
      sqlcmd.Connection.Open();

      using var reader = sqlcmd.ExecuteReader();
      return reader.ParseAsync<SpParameter>(mappers).Result;
    }

    internal static IEnumerable<SpInfo> Read(ICollectionSpToEntity mappers, string conStr)
    {
      var parameters = ReadSpParameter(mappers, conStr);
      var ret = new HashSet<SpInfo>();
      var map = mappers.FirstOrDefault(Constant.TypeName<SpProperty>());

      using var sqlcmd = new SqlCommand(Constant.APP.DotAnd(Constant.TypeName<SpProperty>().UnderscoreAnd(Constant.READ)), new SqlConnection(conStr))
      {
        CommandType = CommandType.StoredProcedure
      };
      sqlcmd.Connection.Open();

      using var reader = sqlcmd.ExecuteReader();
      while (reader.Read())
      {
        var prop = (map == null) ? mappers.Add<SpProperty>(reader, out map) : map.Parse<SpProperty>(reader);
        var pars = parameters.Where(p => p.SpId == prop.Id);

        ret.Add(new SpInfo(prop, pars));
      }

      return ret;
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
}