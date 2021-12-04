using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TimeManageData.Models;

namespace TimeManageData.Repositories
{
    public class UserMockRepository : IRepository<ApplicationUser>
    {
        private readonly List<ApplicationUser> _users;

        public UserMockRepository()
        {
            _users = new List<ApplicationUser>
            {
                new()
                {
                    Id = "afdfauiewhfkj",
                    ActiveTimeStartSeconds = 5000,
                    ActiveTimeEndSeconds = 72000,
                    Tasks = new List<UserTask>()
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
                        }
                    }
                },
                new()
                {
                    Id = "af323faedsfsd",
                    ActiveTimeStartSeconds = 2000,
                    ActiveTimeEndSeconds = 72000,
                    Tasks = new List<UserTask>()
                    {
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
                        }
                    }
                },
                new()
                {
                    Id = "bys54btswbrsa43vt",
                    ActiveTimeStartSeconds = 15000,
                    ActiveTimeEndSeconds = 72000,
                    Tasks = new List<UserTask>()
                    {
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
                        }
                    }
                },
            };
        }

        public Task SaveChangesAsync()
        {
            return Task.Run(() => Thread.Sleep(1000));
        }

        public ApplicationUser Create(ApplicationUser item)
        {
            if (item is null || _users.Any(t => t.Id == item.Id))
            {
                throw new ArgumentException(nameof(item));
            }

            _users.Add(item);

            return item;
        }

        public ApplicationUser Find(string id)
        {
            return _users.FirstOrDefault(t => t.Id == id);
        }

        public ApplicationUser Update(ApplicationUser item)
        {
            ApplicationUser userToPut = _users.FirstOrDefault(t => t.Id == item.Id);

            if (userToPut is null)
            {
                throw new ArgumentException(nameof(item));
            }

            userToPut.ActiveTimeEndSeconds = item.ActiveTimeEndSeconds;
            userToPut.ActiveTimeStartSeconds = item.ActiveTimeStartSeconds;

            return userToPut;
        }

        public ApplicationUser Delete(string id)
        {
            ApplicationUser userToDelete = _users.FirstOrDefault(t => t.Id == id);

            if (userToDelete is null)
            {
                throw new ArgumentException(nameof(id));
            }

            _users.Remove(userToDelete);

            return userToDelete;
        }

        public List<ApplicationUser> GetAll()
        {
            return _users;
        }
    }
}
