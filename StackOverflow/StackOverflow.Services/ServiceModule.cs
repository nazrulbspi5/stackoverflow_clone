using Autofac;
using StackOverflow.DAL.NHibernate;
using StackOverflow.Services.Authentication;
using StackOverflow.Services.Services.Authentication;

namespace StackOverflow.Services
{
    public class ServiceModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<SessionManagerFactory>().As<ISessionManagerFactory>().
            //    WithParameter("connectionString", _connectionString).
            //    InstancePerLifetimeScope();

            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}