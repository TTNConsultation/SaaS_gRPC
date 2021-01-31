using System.Security.Claims;
using DbContext.Interface;
using DbContext.Command;
using Google.Protobuf;

namespace DbContext
{
  public sealed class StoreProcedure : IDbContext
  {
    private readonly IConnectionManager _connections;
    private readonly ICollectionMapper _mappers;
    private readonly ICollectionProcedure _procedures;

    public StoreProcedure(IConnectionManager conmanager, ICollectionMapper maps, ICollectionProcedure procedures)
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
      RootId = int.Parse(conmng.Get(Constant.APPID));

      if (cp.IsInRole(Constant.ADMIN))
        ConnectionString = conmng.Get(Constant.ADMIN);
      else if (cp.IsInRole(Constant.USER))
        ConnectionString = conmng.Get(Constant.USER);
      else if (cp.IsInRole(Constant.APP))
        ConnectionString = conmng.Get(Constant.APP);
    }

    internal Security(string conStr, int rootId)
    {
      ConnectionString = conStr;
      RootId = rootId;
    }
  }
}