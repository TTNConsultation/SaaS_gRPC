using System.Collections.Generic;

namespace Protos.Shared.Message.Administrator
{
  public partial class Restaurants
  {
    public Restaurants(ICollection<Restaurant> values)
    {
      if (values != null)
        Values.AddRange(values);
    }
  }

  public partial class Items
  {
    public Items(ICollection<Item> values)
    {
      if (values != null)
        Values.AddRange(values);
    }
  }

  public partial class RestaurantMenus
  {
    public RestaurantMenus(ICollection<RestaurantMenu> values)
    {
      if (values != null)
        Values.AddRange(values);
    }
  }

  public partial class Menus
  {
    public Menus(ICollection<Menu> values)
    {
      if (values != null)
        Values.AddRange(values);
    }
  }

  public partial class MenuItems
  {
    public MenuItems(ICollection<MenuItem> values)
    {
      if (values != null)
        Values.AddRange(values);
    }
  }

  public partial class Tables
  {
    public Tables(ICollection<Table> values)
    {
      if (values != null)
        Values.AddRange(values);
    }
  }
}