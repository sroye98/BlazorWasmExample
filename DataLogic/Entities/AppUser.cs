using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DataLogic.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public AppUser()
        {
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get => $"{FirstName} {LastName}"; }

        public virtual ICollection<AppUserClaim> Claims { get; set; }

        public virtual ICollection<AppUserLogin> Logins { get; set; }

        public virtual ICollection<AppUserToken> Tokens { get; set; }

        public virtual ICollection<AppUserRole> UserRoles { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }    
    }
}
