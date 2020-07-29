using System;

namespace Dal.Sp
{
  public interface IWrite<T> : IDisposable where T : new()
  {
    string Error();

    int RootId();

    bool IsReady();

    int Create(T obj);

    bool Update(T obj);

    bool UpdateState(int id, int stateId);

    bool Delete(int id);
  }

  internal sealed class Write<T> : Base<T>, IWrite<T> where T : new()
  {
    private readonly IReadOnly<T> SpRO;

    public Write(DbContext.UserClaim claim, ISpInfo sp, ISpInfo spReadOnly, IMapper map) : base(claim, sp, map)
    {
      SpRO = (spReadOnly == null) ? null : new ReadOnly<T>(claim, spReadOnly, map);
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