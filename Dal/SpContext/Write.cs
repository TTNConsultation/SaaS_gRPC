using System;

namespace Dal.Sp
{
  public interface IWrite<T> : IDisposable where T : new()
  {
    bool IsReady();

    string Error();

    int Create(T obj);

    bool Update(T obj);

    bool UpdateState(int id, int stateId);

    bool Delete(int id);
  }

  internal sealed class Write<T> : Base<T>, IWrite<T> where T : new()
  {
    private readonly IReadOnly<T> SpRonly;

    public Write(Context.UserClaim claim, ISpInfo sp, ISpInfo spReadOnly, ICollectionMapper mappers) : base(claim, sp, mappers)
    {
      SpRonly = (spReadOnly == null) ? null : new ReadOnly<T>(claim, spReadOnly, mappers);
    }

    public int Create(T obj) => AddParameters(obj) ? Create() : -1;

    public bool Update(T obj) => AddParameters(obj) && Update();

    public bool UpdateState(int id, int stateId) => AddParameters(SpRonly.Read(id)) && AddParameter(Constant.STATE.Id(), stateId) && Update();

    public bool Delete(int id) => AddParameter(Constant.ID, id) && Update();

    public override void Dispose()
    {
      SpRonly?.Dispose();
      base.Dispose();
    }
  }
}