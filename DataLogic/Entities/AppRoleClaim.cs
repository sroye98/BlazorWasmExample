using System;
using Microsoft.AspNetCore.Identity;

namespace DataLogic.Entities
{
    public class AppRoleClaim : IdentityRoleClaim<Guid>
    {
        public AppRoleClaim()
        {
        }

        public virtual AppRole Role { get; set; }
    }
}
