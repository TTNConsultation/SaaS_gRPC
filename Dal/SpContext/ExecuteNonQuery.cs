using Google.Protobuf;
using System;

namespace Dal.Sp
{
  public interface IExecuteNonQuery<T> : IDisposable where T : IMessage, new()
  {
    string Error();

    int RootId();

    bool IsReady();

    int Create(T obj);

    bool Update(T obj);

    bool UpdateState(int id, int stateId);

    bool Delete(int id);
  }

  internal sealed class ExecuteNonQuery<T> : DbCommand<T>, IExecuteNonQuery<T> where T : IMessage, new()
  {
    private readonly IExecuteReader<T> SpRO;

    public ExecuteNonQuery(DbContext.UserClaim claim, ISpProperty sp, ISpProperty spReadOnly, ICollectionMapper reflectionMaps) : base(claim, sp, reflectionMaps)
    {
      SpRO = (spReadOnly == null) ? null : new ExecuteReader<T>(claim, spReadOnly, reflectionMaps);
    }

    public int Create(T obj) => AddParameters(obj) ? Create() : -1;

    public bool Update(T obj) => AddParameters(obj) && Update();

    public bool UpdateState(int id, int stateId) => AddParameters(SpRO.Read(id)) && AddParameter(Constant.STATE.Id(), stateId) && Update();

    public bool Delete(int id) => AddParameter(Constant.ID, id) && Update();

    public override void Dispose()
    {
      SpRO?.Dispose();
      base.Dispose();
    }
  }
}