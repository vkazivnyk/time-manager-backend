using System.Collections.Generic;
using System.Threading.Tasks;
using TimeManageData.DbContexts;
using TimeManageData.Models;

namespace TimeManageData.Repositories
{
    public class UserRepository : IRepository<ApplicationUser>
    {
        private readonly TimeManagerDbContext _dbContext;

        public UserRepository(TimeManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task SaveChangesAsync()
        {
            return default;
        }

        public void Create(ApplicationUser item)
        {

        }

        public ApplicationUser Find(string id)
        {
            return default;
        }

        public bool Update(string id, ApplicationUser item)
        {
            return default;
        }

        public bool Delete(string id)
        {
            return default;
        }

        public List<ApplicationUser> GetAll()
        {
            return default;
        }
    }
}
