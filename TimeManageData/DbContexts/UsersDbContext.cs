using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimeManageData.Models;

namespace TimeManageData.DbContexts
{
    public class UsersDbContext : IdentityDbContext<ApplicationUser>
    {
        public override DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ApplicationUser>()
                .HasMany(u => u.Tasks)
                .WithOne(t => t.ApplicationUser)
                .HasForeignKey(t => t.UserId);

            modelBuilder
                .Entity<Task>()
                .HasOne(t => t.ApplicationUser)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
