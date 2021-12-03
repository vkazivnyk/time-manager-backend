using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using TimeManageData.DbContexts;
using TimeManageData.Models;
using Task = TimeManageData.Models.Task;

namespace TimeManagerWebAPI.GraphQL.Tasks
{
    public class TaskType : ObjectType<Task>
    {
        protected override void Configure(IObjectTypeDescriptor<Task> descriptor)
        {
            descriptor.Description("Represents a task that needs to be completed.");

            descriptor
                .Field(t => t.UserId)
                .Ignore();

            descriptor
                .Field(t => t.ApplicationUser)
                .ResolveWith<Resolvers>(r => Resolvers.GetUser(default, default))
                .UseDbContext<TimeManagerDbContext>()
                .Description("Represents the user to whom the task belongs to.");
        }

        private class Resolvers
        {
            [UseDbContext(typeof(TimeManagerDbContext))]
            public static ApplicationUser GetUser(Task task, [Service] TimeManagerDbContext context)
            {
                return context.Users.FirstOrDefault(u => u.Id == task.UserId);
            }
        }
    }
}
