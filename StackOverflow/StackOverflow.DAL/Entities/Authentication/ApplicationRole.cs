using FluentNHibernate.AspNetCore.Identity;

namespace StackOverflow.DAL.Entities.Authentication
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole()
            : base()
        {
        }

        public ApplicationRole(string roleName)
            : base(roleName)
        {
        }
    }
}