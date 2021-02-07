using Constant;
using System.Collections.Generic;
using System.Linq;

namespace Protos.Message.Reference
{
  public partial class States
  {
    public int DeleteId => Values.First(s => s.Name.IsEqual(StrVal.DELETE)).Id;
    public int EnableId => Values.First(s => s.Name.IsEqual(StrVal.ENABLE)).Id;
    public int DisableId => Values.First(s => s.Name.IsEqual(StrVal.DISABLE)).Id;

    public States(IEnumerable<State> values) => Values.AddRange(values);
  }

  public partial class KeyTypes
  {
    public KeyTypes(ICollection<KeyType> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class References
  {
    public AppSetting AppSetting { get; set; }
  }
}