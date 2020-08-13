using Google.Protobuf;
using System.Security.Claims;

namespace Dal.Sp
{
  public interface IDbContext
  {
    IExecuteReader<T> ReferenceData<T>(int rootId = 0) where T : IMessage, new();

    IExecuteReader<T> ReadContext<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : IMessage, new();

    IExecuteNonQuery<T> WriteContext<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : IMessage, new();
  }

  public sealed class DbContext : IDbContext
  {
    private readonly IConnectionManager ConnectionStrings;
    private readonly ICollectionMapper ReflectionMaps;
    private readonly ICollectionSpProperty SpProperties;

    public DbContext(IConnectionManager conmanager, ICollectionMapper mappers, ICollectionSpProperty spProps)
    {
      ConnectionStrings = conmanager;
      ReflectionMaps = mappers;
      SpProperties = spProps;
    }

    public IExecuteReader<T> ReferenceData<T>(int rootId) where T : IMessage, new() =>
      new ExecuteReader<T>(UserClaim.AppUser(ConnectionStrings, rootId),
                      SpProperties.Get<T>(OperationType.R),
                      ReflectionMaps);

    public IExecuteReader<T> ReadContext<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : IMessage, new() =>
      new ExecuteReader<T>(UserClaim.Get(ConnectionStrings, uc, appId),
                      SpProperties.Get<T>(op),
                      ReflectionMaps);

    public IExecuteNonQuery<T> WriteContext<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : IMessage, new() =>
      new ExecuteNonQuery<T>(UserClaim.Get(ConnectionStrings, uc, appId),
                   SpProperties.Get<T>(op),
                   SpProperties.Get<T>(OperationType.R),
                   ReflectionMaps);

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