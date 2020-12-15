using System.Collections.Generic;

namespace Saas.Message.Administrator
{
  public partial class Restaurants
  {
    public Restaurants(IEnumerable<Restaurant> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class Items
  {
    public Items(IEnumerable<Item> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class RestaurantMenus
  {
    public RestaurantMenus(IEnumerable<RestaurantMenu> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class Menus
  {
    public Menus(IEnumerable<Menu> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class MenuItems
  {
    public MenuItems(IEnumerable<MenuItem> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class Tables
  {
    public Tables(IEnumerable<Table> values)
    {
      Values.AddRange(values);
    }
  }
}