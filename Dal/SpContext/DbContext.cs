using System.Security.Claims;

namespace Dal.Sp
{
  public interface IDbContext : IObject
  {
    IReadOnly<T> ReferenceData<T>(int rootId = 0) where T : new();

    IReadOnly<T> ReadOnly<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : new();

    IWrite<T> Write<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : new();
  }

  public sealed class DbContext : IDbContext
  {
    private readonly IConnectionManager ConnectionStrings;
    private readonly ICollectionMapper Mappers;
    private readonly ICollectionSpInfo SpInfos;

    public DbContext(IConnectionManager conmanager, ICollectionMapper mappers, ICollectionSpInfo spinfos)
    {
      ConnectionStrings = conmanager;
      Mappers = mappers;
      SpInfos = spinfos;
    }

    public IReadOnly<T> ReferenceData<T>(int rootId) where T : new() =>
      new ReadOnly<T>(UserClaim.AppUser(ConnectionStrings, rootId),
                      SpInfos.Get<T>(OperationType.R),
                      Mappers.Get<T>());

    public IReadOnly<T> ReadOnly<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : new() =>
      new ReadOnly<T>(UserClaim.Get(ConnectionStrings, uc, appId),
                      SpInfos.Get<T>(op),
                      Mappers.Get<T>());

    public IWrite<T> Write<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : new() =>
      new Write<T>(UserClaim.Get(ConnectionStrings, uc, appId),
                   SpInfos.Get<T>(op),
                   SpInfos.Get<T>(OperationType.R),
                   Mappers.Get<T>());

    public bool IsNotNull() => ConnectionStrings.IsNotNull() && SpInfos.IsNotNull();

    public sealed class UserClaim
    {
      internal readonly int RootId;
      internal readonly string ConnectionString;

      private UserClaim(IConnectionManager conManager, ClaimsPrincipal cp, int appId)
      {
        foreach (var c in cp.Claims)
        {
          switch (c.Type)
          {
            case Constant.CLIENT_ID:
              RootId = int.Parse(c.Value);
              break;

            case Constant.ROLE:
              ConnectionString = conManager.Get(c.Value);
              break;
          }
        }

        if (RootId <= 0)
          RootId = appId;
      }

      private UserClaim(string conStr, int rootId)
      {
        ConnectionString = conStr;
        RootId = rootId;
      }

      internal static UserClaim Get(IConnectionManager conmanager, ClaimsPrincipal uc, int appid = 0) => new UserClaim(conmanager, uc, appid);

      internal static UserClaim AppUser(IConnectionManager conManager, int rootId) => new UserClaim(conManager.App(), rootId);
    }
  }
}