using FluentNHibernate.AspNetCore.Identity;

namespace StackOverflow.DAL.Entities.Authentication
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual string? FirstName { get; set; }
        public virtual string? LastName { get; set; }
    }
}