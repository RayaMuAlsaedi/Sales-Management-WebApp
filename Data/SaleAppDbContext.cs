using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SaleInvoicesApp.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SaleInvoicesApp.Data
{
    public class SaleAppDbContext : IdentityDbContext<SalesStaff>
    {
        private readonly DbContextOptions _options;

        public SaleAppDbContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        public DbSet<Invoice> Invoices { get; set; } = default!;

        public DbSet<Inventory> Inventory { get; set; } = default!;

        public DbSet<Category> Categories { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.HasDefaultSchema("GolfClubDb");
            builder.Entity<SalesStaff>(entity =>
            {
                entity.ToTable(name: "SalesStaff");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

        }
    }
}
