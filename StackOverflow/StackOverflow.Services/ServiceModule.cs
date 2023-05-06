using Autofac;
using StackOverflow.DAL.NHibernate;
using StackOverflow.Services.Authentication;


namespace StackOverflow.Services
{
    public class ServiceModule:Module
    {
        private readonly string _connectionString;
        public ServiceModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<SessionManagerFactory>().As<ISessionManagerFactory>().
            //    WithParameter("connectionString", _connectionString).
            //    InstancePerLifetimeScope();

            //builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}