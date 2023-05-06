using Autofac;
using StackOverflow.DAL.NHibernate;

namespace StackOverflow.DAL
{
    public class DalModule:Module
    {
        private readonly string _connectionString;
        public DalModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SessionManagerFactory>().AsSelf().
                WithParameter("connectionString", _connectionString).
                InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}