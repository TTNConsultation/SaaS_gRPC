using Google.Protobuf;
using System.Security.Claims;

using StoreProcedure.Interface;
using StoreProcedure.Command;

namespace StoreProcedure
{  
  public sealed class DbContext : IDbContext
  {
    private readonly IConnectionManager _connectionManager;
    private readonly ICollectionMapper _mappers;
    private readonly ICollectionStoreProcedure _storeProcedures;

    public DbContext(IConnectionManager conmanager, ICollectionMapper mappers, ICollectionStoreProcedure sp)
    {
      _connectionManager = conmanager;
      _mappers = mappers;
      _storeProcedures = sp;
    }

    public IExecuteReader<T> ReferenceData<T>(int rootId) where T : IMessage, new() =>
      new ExecuteReader<T>(new Security(_connectionManager.App(), rootId),
                      _storeProcedures.Get<T>(OperationType.R),
                      _mappers);

    public IExecuteReader<T> Read<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : IMessage, new() =>
      new ExecuteReader<T>(new Security(_connectionManager, uc, appId),
                      _storeProcedures.Get<T>(op),
                      _mappers);

    public IExecuteNonQuery<T> Write<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : IMessage, new() =>
      new ExecuteNonQuery<T>(new Security(_connectionManager, uc, appId),
                   _storeProcedures.Get<T>(op),
                   _storeProcedures.Get<T>(OperationType.R),
                   _mappers);
  }

  internal sealed class Security
  {
    public readonly int RootId;
    public readonly string ConnectionString;     

    public Security(IConnectionManager conManager, ClaimsPrincipal cp, int appId)
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
    public Security(string conStr, int rootId)
    {
      ConnectionString = conStr;
      RootId = rootId;
    }    
  }
}