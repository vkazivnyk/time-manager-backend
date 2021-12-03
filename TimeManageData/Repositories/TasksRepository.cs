using TimeManageData.DbContexts;

namespace TimeManageData.Repositories
{
    public class TasksRepository
    {
        private readonly UsersDbContext _dbContext;

        public TasksRepository(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
