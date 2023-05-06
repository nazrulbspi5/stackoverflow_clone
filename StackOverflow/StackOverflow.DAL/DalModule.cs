using Autofac;
using StackOverflow.DAL.Entities.Authentication;
using StackOverflow.DAL.NHibernate;
using StackOverflow.DAL.UnitOfWorks;

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
            builder.RegisterType<ApplicationUser>().AsSelf();

            builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
           .InstancePerLifetimeScope();


            base.Load(builder);
        }
    }
}