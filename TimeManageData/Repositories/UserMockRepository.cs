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
                    Tasks = new List<UserTask>()
                    {
                        new()
                        {
                            Id = "afh894afjknn39",
                            Deadline = DateTime.Now.AddDays(2),
                            Importance = 1,
                            Name = "FirstTask",
                            Difficulty = 2,
                            User = new ApplicationUser()
                            {
                                Id = "afdfauiewhfkj",
                            },
                            UserId = "afdfauiewhfkj"
                        }
                    }
                },
                new()
                {
                    Id = "af323faedsfsd",
                    Tasks = new List<UserTask>()
                    {
                        new()
                        {
                            Id = "gdfsgs45gdfgs4",
                            Deadline = DateTime.Now.AddDays(7),
                            Importance = 1,
                            Name = "SecondTask",
                            Difficulty = 0,
                            User = new ApplicationUser()
                            {
                                Id = "af323faedsfsd",
                            },
                            UserId = "af323faedsfsd"
                        }
                    }
                },
                new()
                {
                    Id = "bys54btswbrsa43vt",
                    Tasks = new List<UserTask>()
                    {
                        new()
                        {
                            Id = "afh894afjk5nn39",
                            Deadline = DateTime.Now.AddDays(20),
                            Importance = 2,
                            Name = "ThirdTask",
                            Difficulty = 1,
                            User = new ApplicationUser()
                            {
                                Id = "bys54btswbrsa43vt",
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
