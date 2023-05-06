using NHibernate;

namespace StackOverflow.DAL.NHibernate
{
    public interface ISessionManagerFactory
    {
        ISession OpenSession();
    }
}
