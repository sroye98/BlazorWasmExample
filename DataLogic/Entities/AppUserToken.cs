using System;
using Microsoft.AspNetCore.Identity;

namespace DataLogic.Entities
{
    public class AppUserToken : IdentityUserToken<Guid>
    {
        public AppUserToken()
        {
        }

        public virtual AppUser User { get; set; }
    }
}
