using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();

        public ApplicationUser Create(ApplicationUser item) => _dbContext.Add(item).Entity;

        public ApplicationUser Find(string id) => _dbContext.Users.First(x => x.Id == id);

        public ApplicationUser Update(ApplicationUser item) => _dbContext.Update(item).Entity;

        public ApplicationUser Delete(string id) => _dbContext.Remove(Find(id)).Entity;

        public List<ApplicationUser> Where(Func<ApplicationUser, bool> predicate) =>
            _dbContext.Users.Where(predicate).ToList();

        public List<ApplicationUser> GetAll() => _dbContext.Users.ToList();
    }
}
