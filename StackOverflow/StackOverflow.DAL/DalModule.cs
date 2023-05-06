using Autofac;

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
            //For Migration
            builder.RegisterType<ApplicationDbContext>().AsSelf().
                WithParameter("connectionString", _connectionString).
               //WithParameter("migrationAssemblyName", _migrationAssemblyName).
                InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}