using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TimeManageData.Models;

namespace TimeManageData.Repositories
{
    public class UserTaskMockRepository : IRepository<UserTask>
    {
        private readonly List<UserTask> _tasks;

        public UserTaskMockRepository()
        {
            _tasks = new List<UserTask>
            {
                new()
                {
                    Id = "afh894afjknn39",
                    Deadline = DateTime.Now.AddDays(2),
                    TotalSeconds = 100,
                    Name = "FirstTask",
                    Difficulty = 2,
                    User = new ApplicationUser()
                    {
                        Id = "afdfauiewhfkj",
                        ActiveTimeStartSeconds = 5000,
                        ActiveTimeEndSeconds = 72000,
                    },
                    UserId = "afdfauiewhfkj"
                },
                new()
                {
                    Id = "gdfsgs45gdfgs4",
                    Deadline = DateTime.Now.AddDays(7),
                    TotalSeconds = 1000,
                    Name = "SecondTask",
                    Difficulty = 0,
                    User = new ApplicationUser()
                    {
                        Id = "af323faedsfsd",
                        ActiveTimeStartSeconds = 2000,
                        ActiveTimeEndSeconds = 72000,
                    },
                    UserId = "af323faedsfsd"
                },
                new()
                {
                    Id = "afh894afjknn39",
                    Deadline = DateTime.Now.AddDays(20),
                    TotalSeconds = 236458,
                    Name = "ThirdTask",
                    Difficulty = 4,
                    User = new ApplicationUser()
                    {
                        Id = "bys54btswbrsa43vt",
                        ActiveTimeStartSeconds = 15000,
                        ActiveTimeEndSeconds = 72000,
                    },
                    UserId = "bys54btswbrsa43vt"
                },
            };
        }

        public Task SaveChangesAsync()
        {
            return Task.Run(() => Thread.Sleep(1000));
        }

        public UserTask Create(UserTask item)
        {
            if (item is null || _tasks.Any(t => t.Id == item.Id))
            {
                throw new ArgumentException(nameof(item));
            }

            _tasks.Add(item);

            return item;
        }

        public UserTask Find(string id)
        {
            return _tasks.FirstOrDefault(t => t.Id == id);
        }

        public UserTask Update(UserTask item)
        {
            UserTask taskToPut = _tasks.FirstOrDefault(t => t.Id == item.Id);

            if (taskToPut is null)
            {
                throw new ArgumentException(nameof(item));
            }

            taskToPut.Deadline = item.Deadline;
            taskToPut.Difficulty = item.Difficulty;
            taskToPut.Name = item.Name;
            taskToPut.TotalSeconds = item.TotalSeconds;

            return taskToPut;
        }

        public UserTask Delete(string id)
        {
            UserTask taskToDelete = _tasks.FirstOrDefault(t => t.Id == id);

            if (taskToDelete is null)
            {
                throw new ArgumentException(nameof(id));
            }

            _tasks.Remove(taskToDelete);

            return taskToDelete;
        }

        public List<UserTask> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
