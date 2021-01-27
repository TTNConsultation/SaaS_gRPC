using System.Security.Claims;
using StoreProcedure.Interface;
using StoreProcedure.Command;
using Google.Protobuf;

namespace StoreProcedure
{
  public sealed class DbContext : IDbContext
  {
    private readonly IConnectionManager _connections;
    private readonly ICollectionMapper _mappers;
    private readonly ICollectionProcedure _procedures;

    public DbContext(IConnectionManager conmanager, ICollectionMapper maps, ICollectionProcedure procedures)
    {
      _connections = conmanager;
      _mappers = maps;
      _procedures = procedures;
    }

    public IExecuteReader<T> ReferenceData<T>(int rootId) where T : IMessage<T>, new() =>
      new ExecuteReader<T>(new Security(_connections.App(), rootId),
                          _procedures.Get<T>(OperationType.R),
                          _mappers.Get<T>());

    public IExecuteReader<T> Read<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : IMessage<T>, new() =>
      new ExecuteReader<T>(new Security(_connections, uc, appId),
                          _procedures.Get<T>(op),
                          _mappers.Get<T>());

    public IExecuteNonQuery<T> Write<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : IMessage<T>, new() =>
      new ExecuteNonQuery<T>(new Security(_connections, uc, appId),
                            _procedures.Get<T>(op),
                            _procedures.Get<T>(OperationType.R),
                            _mappers.Get<T>());
  }

  internal sealed class Security
  {
    public readonly int RootId;
    public readonly string ConnectionString;

    public Security(IConnectionManager conmng, ClaimsPrincipal cp, int appid)
    {
      RootId = appid;

      foreach (var c in cp.Claims)
      {
        switch (c.Type)
        {
          case Constant.CLIENT_ID:
            RootId = int.Parse(c.Value);
            break;

          case Constant.ROLE:
            ConnectionString = conmng.Get(c.Value);
            break;
        }
      }
    }

    public Security(string conStr, int rootId)
    {
      ConnectionString = conStr;
      RootId = rootId;
    }
  }
}