using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimeManageData.Models;

namespace TimeManageData.DbContexts
{
    public class TimeManagerDbContext : IdentityDbContext<ApplicationUser>
    {
        public override DbSet<ApplicationUser> Users { get; set; }

        public DbSet<UserTask> UserTasks { get; set; }

        public TimeManagerDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=time-manager;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ApplicationUser>()
                .HasMany(u => u.Tasks)
                .WithOne(t => t.ApplicationUser)
                .HasForeignKey(t => t.UserId);

            modelBuilder
                .Entity<UserTask>()
                .HasOne(t => t.ApplicationUser)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
