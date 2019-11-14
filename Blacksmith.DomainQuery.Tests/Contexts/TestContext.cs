using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blacksmith.PagedEnumerable.Tests.Contexts
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Roles)
                .WithOne(ur => ur.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<User>()
                .HasIndex(u => u.Name)
                .IsUnique();

            modelBuilder
                .Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(ur => ur.Role)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder
                .Entity<UserRole>()
                .HasAlternateKey(ur => new { ur.UserId, ur.RoleId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
