using FluentNHibernate.AspNetCore.Identity.Mappings;
using StackOverflow.DAL.Entities.Authentication;

namespace StackOverflow.DAL.Mapping.Authentication;

public class ApplicationUserMap : IdentityUserMapBase<ApplicationUser, Guid>
{
    public ApplicationUserMap() : base(t => t.GeneratedBy.Guid()) // Primary key config
    {
        Map(x => x.FirstName).Not.Nullable();
        Map(x => x.LastName).Not.Nullable();
    }
}
