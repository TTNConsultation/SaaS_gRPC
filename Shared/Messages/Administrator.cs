using System.Collections.Generic;

namespace Protos.Message.Administrator
{
  public partial class Restaurants
  {
    public Restaurants(ICollection<Restaurant> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class Items
  {
    public Items(ICollection<Item> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class RestaurantMenus
  {
    public RestaurantMenus(ICollection<RestaurantMenu> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class Menus
  {
    public Menus(ICollection<Menu> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class MenuItems
  {
    public MenuItems(ICollection<MenuItem> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class Tables
  {
    public Tables(ICollection<Table> values)
    {
      Values.AddRange(values);
    }
  }
}