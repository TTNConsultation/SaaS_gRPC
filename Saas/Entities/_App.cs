using System.Collections.Generic;
using System.Globalization;

using System.Linq;

using Dal;

namespace Saas.Entity.App
{
  public partial class States
  {
    public int DeleteId => Values.First(s => string.Compare(s.Name, Constant.DELETE, true, CultureInfo.CurrentCulture) == 0).Id;
    public int EnableId => Values.First(s => string.Compare(s.Name, Constant.ENABLE, true, CultureInfo.CurrentCulture) == 0).Id;
    public int DisableId => Values.First(s => string.Compare(s.Name, Constant.DISABLE, true, CultureInfo.CurrentCulture) == 0).Id;

    public States(IEnumerable<Types.State> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class Languages
  {
    public Languages(IEnumerable<Types.Language> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class Keys
  {
    public Keys(IEnumerable<Types.Key> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class French
  {
    public French(IEnumerable<Types.Value> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class English
  {
    public English(IEnumerable<Types.Value> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class Vietnamese
  {
    public Vietnamese(IEnumerable<Types.Value> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class KeyTypes
  {
    public KeyTypes(IEnumerable<Types.KeyType> values)
    {
      Values.AddRange(values);
    }
  }
}