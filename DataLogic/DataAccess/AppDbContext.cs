using System;
using DataLogic.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLogic.DataAccess
{
    public class AppDbContext : IdentityDbContext<AppUser,
        AppRole,
        Guid,
        AppUserClaim,
        AppUserRole,
        AppUserLogin,
        AppRoleClaim,
        AppUserToken> 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<AppRole> AppRoles { get; set; }

        public DbSet<AppRoleClaim> AppRoleClaims { get; set; }

        public DbSet<AppUserClaim> AppUserClaims { get; set; }

        public DbSet<AppUserLogin> AppUserLogins { get; set; }

        public DbSet<AppUserRole> AppUserRoles { get; set; }

        public DbSet<AppUserToken> AppUserTokens { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>(b =>
            {
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                b.HasMany(e => e.RefreshTokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId);

                b.Property(u => u.UserName).HasMaxLength(256);

                b.Property(u => u.NormalizedUserName).HasMaxLength(256);

                b.Property(u => u.Email).HasMaxLength(256);

                b.Property(u => u.NormalizedEmail).HasMaxLength(256);

                b.Property(u => u.PhoneNumber).HasMaxLength(50);

                b.ToTable("AppUsers");
            });

            modelBuilder.Entity<AppRole>(b =>
            {
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();

                b.Property(t => t.Name).HasMaxLength(128);

                b.ToTable("AppRoles");
            });

            modelBuilder.Entity<AppUserToken>(b =>
            {
                b.Property(t => t.LoginProvider).HasMaxLength(256);

                b.Property(t => t.Name).HasMaxLength(256);

                b.ToTable("AppUserTokens");
            });

            modelBuilder.Entity<AppUserLogin>(b =>
            {
                b.Property(t => t.LoginProvider).HasMaxLength(256);

                b.Property(t => t.ProviderDisplayName).HasMaxLength(256);

                b.ToTable("AppUserLogins");
            });

            modelBuilder.Entity<AppUserClaim>(b =>
            {
                b.Property(t => t.ClaimType).HasMaxLength(450);

                b.ToTable("AppUserClaims");
            });

            modelBuilder.Entity<AppRoleClaim>(b =>
            {
                b.Property(t => t.ClaimType).HasMaxLength(450);

                b.ToTable("AppRoleClaims");
            });
        }
    }
}
