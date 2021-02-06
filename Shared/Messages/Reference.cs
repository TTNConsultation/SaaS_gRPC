using Protos.Message.Language;
using System;
using System.Collections.Generic;
using System.Linq;

using Constant;

namespace Protos.Message.Reference
{
  public partial class States
  {
    public int DeleteId => Values.First(s => s.Name.IsEqual("Delete")).Id;
    public int EnableId => Values.First(s => s.Name.IsEqual("Enable")).Id;
    public int DisableId => Values.First(s => s.Name.IsEqual("Disable")).Id;

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