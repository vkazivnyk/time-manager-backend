using TimeManageData.DbContexts;

namespace TimeManageData.Repositories
{
    public class TimeManageRepository
    {
        private readonly TimeManagerDbContext _dbContext;

        public TimeManageRepository(TimeManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
