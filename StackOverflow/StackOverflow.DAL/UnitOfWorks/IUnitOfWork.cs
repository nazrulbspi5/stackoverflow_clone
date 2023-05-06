namespace StackOverflow.DAL.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    void Flush();
    void BeginTransaction();
    void Commit();
    void RollBack();
}
