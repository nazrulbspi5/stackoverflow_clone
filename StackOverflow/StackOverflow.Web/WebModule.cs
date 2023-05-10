using Autofac;
using StackOverflow.Web.Models;

namespace StackOverflow.Web
{
    public class WebModule :Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LoginModel>().AsSelf();
            builder.RegisterType<RegisterModel>().AsSelf();
            base.Load(builder);
        }
    }
}
