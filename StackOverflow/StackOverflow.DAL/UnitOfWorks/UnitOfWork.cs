using NHibernate;

namespace StackOverflow.DAL.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{

    private static readonly ISessionFactory _sessionFactory;
    private ITransaction _transaction;
    public ISession Session { get; private set; }

    protected ISession _session;

    public UnitOfWork()
    {
        _session = _sessionFactory.OpenSession();
    }

    public void Flush()
    {
        _session.Flush();
    }

    public void BeginTransaction()
    {
        _transaction = Session.BeginTransaction();
    }

    public void Commit()
    {
        try
        {
            // commit transaction if there is one active
            if (_transaction != null && _transaction.IsActive)
                _transaction.Commit();
        }
        catch
        {
            // rollback if there was an exception
            if (_transaction != null && _transaction.IsActive)
                _transaction.Rollback();

            throw;
        }
        finally
        {
            Session.Dispose();
        }
    }

    public void RollBack()
    {

        try
        {
            if (_transaction != null && _transaction.IsActive)
                _transaction.Rollback();
        }
        finally
        {
            Session.Dispose();
        }
    }  
   
}
