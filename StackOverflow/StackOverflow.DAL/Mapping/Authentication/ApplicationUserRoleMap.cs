using FluentNHibernate.Mapping;
using StackOverflow.DAL.Entities.Authentication;

namespace StackOverflow.DAL.Mapping.Membership;

public class ApplicationUserRoleMap : ClassMap<ApplicationUserRole>
{
    public ApplicationUserRoleMap()
    {
        Table("AspNetUserRoles");
        Id(x => x.Id).GeneratedBy.Identity();
        References(x => x.ApplicationUser)
            .Not.Nullable()
            .Cascade.SaveUpdate()
            .Column("UserId");
        References(x => x.Role)
            .Not.Nullable()
            .Cascade.SaveUpdate()
            .Column("RoleId");
    }
}
