using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using TimeManageData.DbContexts;
using TimeManageData.Models;
using TimeManageData.Repositories;

namespace TimeManagerWebAPI.GraphQL.Tasks
{
    public class UserTaskType : ObjectType<UserTask>
    {
        protected override void Configure(IObjectTypeDescriptor<UserTask> descriptor)
        {
            descriptor.Description("Represents a task that needs to be completed.");

            descriptor
                .Field(t => t.UserId)
                .Ignore();

            descriptor
                .Field(t => t.User)
                .ResolveWith<Resolvers>(r => Resolvers.GetUser(default, default))
                .UseDbContext<TimeManagerDbContext>()
                .Description("Represents the user to whom the task belongs to.");
        }

        private class Resolvers
        {
            public static ApplicationUser GetUser(UserTask task, [Service] IRepository<ApplicationUser> userRepo)
            {
                return userRepo.GetAll().FirstOrDefault(u => u.Id == task.UserId);
            }
        }
    }
}
