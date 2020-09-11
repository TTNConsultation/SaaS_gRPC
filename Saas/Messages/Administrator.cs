using System.Collections.Generic;

namespace Saas.Message.Administrator
{
  public partial class Restaurants
  {
    public Restaurants(IEnumerable<Types.Restaurant> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class Items
  {
    public Items(IEnumerable<Types.Item> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class RestaurantMenus
  {
    public RestaurantMenus(IEnumerable<Types.RestaurantMenu> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class Menus
  {
    public Menus(IEnumerable<Types.Menu> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class MenuItems
  {
    public MenuItems(IEnumerable<Types.MenuItem> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class Tables
  {
    public Tables(IEnumerable<Types.Table> values)
    {
      Values.AddRange(values);
    }
  }
}