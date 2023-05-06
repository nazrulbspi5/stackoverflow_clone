using NHibernate;
using StackOverflow.DAL.Repositories;

namespace StackOverflow.DAL.UnitOfWorks;

public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
{
    public ApplicationUnitOfWork(ISession session) : base(session)
    {
      
    }
}
