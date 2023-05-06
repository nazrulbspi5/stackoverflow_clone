using FluentNHibernate.Mapping;
using StackOverflow.DAL.Entities.Authentication;

namespace StackOverflow.DAL.Mapping.Membership;

public class ApplicationRoleMap : ClassMap<ApplicationRole>
{
    public ApplicationRoleMap()
    {
        Table("AspNetRoles");
        Id(x => x.Id).GeneratedBy.Guid();
        Map(x => x.Name);
        Map(x => x.NormalizedName);
    }
}
