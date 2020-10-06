using System;
using Microsoft.AspNetCore.Identity;

namespace DataLogic.Entities
{
    public class AppUserLogin : IdentityUserLogin<Guid>
    {
        public AppUserLogin()
        {
        }

        public virtual AppUser User { get; set; }
    }
}
