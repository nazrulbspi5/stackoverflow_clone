using FluentNHibernate.AspNetCore.Identity;

namespace StackOverflow.DAL.Entities.Authentication
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public virtual int Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;
        public virtual ApplicationRole Role { get; set; } = null!;
    }
}