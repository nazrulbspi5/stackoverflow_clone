using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StackOverflow.DAL.Entities.Authentication;
using Microsoft.Extensions.Logging;

namespace StackOverflow.Services.Authentication
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole> store,
            IEnumerable<IRoleValidator<ApplicationRole>> roleValidators,ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,ILogger<RoleManager<ApplicationRole>> logger):base(store,roleValidators,keyNormalizer,errors,logger)
        {

        }
    }
}
