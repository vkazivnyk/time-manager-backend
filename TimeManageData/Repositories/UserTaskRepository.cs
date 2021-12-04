using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeManageData.DbContexts;
using TimeManageData.Models;

namespace TimeManageData.Repositories
{
    public class UserTaskRepository : IRepository<UserTask>
    {
        private readonly TimeManagerDbContext _dbContext;

        public UserTaskRepository(TimeManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();

        public UserTask Create(UserTask item) => _dbContext.Add(item).Entity;

        public UserTask Find(string id) => _dbContext.UserTasks.First(x => x.Id == id);

        public UserTask Update(UserTask item) => _dbContext.Update(item).Entity;

        public UserTask Delete(string id) => _dbContext.Remove(Find(id)).Entity;

        public List<UserTask> Where(Func<UserTask, bool> predicate) =>
            _dbContext.UserTasks.Where(predicate).ToList();

        public List<UserTask> GetAll() => _dbContext.UserTasks.ToList();
    }
}
