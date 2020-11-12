using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataLogic.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DataLogic.Entities
{
    public class AppUser : IdentityUser<Guid>, IEntity
    {
        public AppUser()
        {
        }

        [Column(TypeName = "nvarchar(256)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string LastName { get; set; }

        public string FullName { get => $"{FirstName} {LastName}"; }

        public virtual ICollection<AppUserClaim> Claims { get; set; }

        public virtual ICollection<AppUserLogin> Logins { get; set; }

        public virtual ICollection<AppUserToken> Tokens { get; set; }

        public virtual ICollection<AppUserRole> UserRoles { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }    
    }
}
