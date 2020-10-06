using System;
using Microsoft.AspNetCore.Identity;

namespace DataLogic.Entities
{
    public class AppUserClaim : IdentityUserClaim<Guid>
    {
        public AppUserClaim()
        {
        }

        public virtual AppUser User { get; set; }
    }
}
