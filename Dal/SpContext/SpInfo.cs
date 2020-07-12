using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dal.Sp
{
  internal sealed class SpInfoManager
  {
    private readonly IEnumerable<SpInfo> SpInfos;

    public SpInfoManager(ICollectionMap mappers, string conStr)
    {
      SpInfos = Read(mappers, conStr);
    }

    public SpInfo Get(string type, OperationType op) => SpInfos.FirstOrDefault(sp => sp.Op.IsEqual(op.ToString()) && sp.Type.IsEqual(type));

    private IEnumerable<SpInfo> Read(ICollectionMap mappers, string conStr)
    {
      var parameters = ReadSpParameter(mappers, conStr);

      string spName = typeof(SpProperty).SpName(Constant.APP, nameof(OperationType.R));

      using var sqlcmd = new SqlCommand(spName, new SqlConnection(conStr))
      {
        CommandType = CommandType.StoredProcedure
      };

      sqlcmd.Connection.Open();
      using var reader = sqlcmd.ExecuteReader();

      var ret = new HashSet<SpInfo>();
      IMap map = null;

      while (reader.Read())
      {
        var prop = (map == null) ? mappers.Add<SpProperty>(reader, out map) : map.Parse<SpProperty>(reader);
        var pars = parameters.Where(p => p.SpId == prop.Id);

        ret.Add(new SpInfo(prop, pars));
      }

      return ret;
    }

    private IEnumerable<SpParameter> ReadSpParameter(ICollectionMap mappers, string conStr)
    {
      string spName = typeof(SpParameter).SpName(Constant.APP, nameof(OperationType.R));
      using var sqlcmd = new SqlCommand(spName, new SqlConnection(conStr))
      {
        CommandType = CommandType.StoredProcedure
      };
      sqlcmd.Connection.Open();

      using var reader = sqlcmd.ExecuteReader();
      return reader.Parse<SpParameter>(mappers);
    }
  }

  internal sealed class SpInfo
  {
    private readonly SpProperty property;
    private readonly IEnumerable<SpParameter> parameters;

    internal int ParameterCount => parameters.Count();

    internal string Op => property.Op;
    internal string Type => property.Base;
    internal string StoreProcName => Schema.DotAnd(property.Name);
    internal string Schema => property.Schema;

    internal SpParameter Parameter(string name) => parameters.FirstOrDefault(p => p.Name.IsEqual(name.AsParameter()));

    internal SpInfo(SpProperty prop, IEnumerable<SpParameter> pars)
    {
      property = prop;
      parameters = pars ?? new HashSet<SpParameter>();
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
                                               : ((value.ToString().Length <= Precision) ? value.ToString().Length
                                                                                         : -1);
    }
  }
}