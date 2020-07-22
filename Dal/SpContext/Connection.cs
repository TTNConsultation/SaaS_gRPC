using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Dal.Sp
{
  public interface IConnectionManager : IObject
  {
    string Get(string schema);

    string App();
  }

  public sealed class ConnectionManager : IConnectionManager
  {
    private readonly IDictionary<string, string> ConnectionStrings;

    public ConnectionManager(IConfiguration config)
    {
      ConnectionStrings = config.GetSection(Constant.CONNECTIONSTRINGS)
                                ?.GetChildren()
                                ?.ToDictionary(s => s.Key, s => s.Value) ?? new Dictionary<string, string>();
    }

    public string Get(string schema) => ConnectionStrings.FirstOrDefault(s => s.Key.IsEqual(schema)).Value;

    public string App() => Get(Constant.APP);

    public bool IsNotNull() => ConnectionStrings.Count > 0;
  }
}