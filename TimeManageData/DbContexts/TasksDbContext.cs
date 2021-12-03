using Microsoft.EntityFrameworkCore;
using TimeManageData.Models;

namespace TimeManageData.DbContexts
{
    public class TasksDbContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }

        public TasksDbContext(DbContextOptions options) : base(options) { }
    }
}
