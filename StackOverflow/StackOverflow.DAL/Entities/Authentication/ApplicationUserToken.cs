using FluentNHibernate.AspNetCore.Identity;
namespace StackOverflow.DAL.Entities.Authentication
{
    public class ApplicationUserToken : IdentityUserToken<Guid>
    {
    }
}
