using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Security.Claims;
using Dal.Sp;

namespace Dal
{
  internal static class RolePolicy
  {
    internal static IEnumerable<Role> Roles => new List<Role>() { new Role(Constant.APP, 1), new Role(Constant.USER, 2), new Role(Constant.EMPLOYEE, 3), new Role(Constant.ADMINISTRATOR, 4) };

    internal static Role GetRole(string name)
    {
      return Roles.FirstOrDefault(r => r.IsRole(name));
    }

    internal static Role GetAppRole()
    {
      return Roles.OrderBy(r => r.Order).First();
    }

    internal sealed class Role
    {
      internal readonly string Name;
      internal readonly int Order;

      internal Role(string name, int order)
      {
        Name = name;
        Order = order;
      }

      internal bool IsUnder(Role role)
      {
        return this.Order <= role.Order;
      }

      internal bool IsRole(string name)
      {
        return Name.IsEqual(name, CultureInfo.CurrentCulture, true);
      }

      public override string ToString()
      {
        return Name;
      }
    }
  }

  internal sealed class User
  {
    internal RolePolicy.Role Role { get; }
    internal int RootId { get; }
    internal int AppId { get; }
    internal bool IdVerified { get; } = false;

    private User(RolePolicy.Role role)
    {
      Role = role;
    }

    internal User(ClaimsPrincipal cp, IAppData appData)
    {
      AppId = appData.AppId();

      foreach (var c in cp.Claims)
      {
        switch (c.Type)
        {
          case "client_id":
            RootId = int.Parse(c.Value);
            break;

          case "role":
            Role = RolePolicy.GetRole(c.Value);
            break;

          case "id":
            var id = int.Parse(c.Value);
            IdVerified = (appData.AppId() == id);
            break;
        }
      }
    }

    internal static User GetAppUser()
    {
      return new User(RolePolicy.GetAppRole());
    }
  }
}