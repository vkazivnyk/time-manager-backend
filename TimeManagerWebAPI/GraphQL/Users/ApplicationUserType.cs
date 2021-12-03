using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using TimeManageData.DbContexts;
using TimeManageData.Models;

namespace TimeManagerWebAPI.GraphQL.Users
{
    public class ApplicationUserType : ObjectType<ApplicationUser>
    {
        protected override void Configure(IObjectTypeDescriptor<ApplicationUser> descriptor)
        {
            descriptor.Description("Represents an application user.");

            descriptor
                .Field(u => u.AccessFailedCount)
                .Ignore();

            descriptor
                .Field(u => u.ConcurrencyStamp)
                .Ignore();

            descriptor
                .Field(u => u.EmailConfirmed)
                .Ignore();

            descriptor
                .Field(u => u.LockoutEnabled)
                .Ignore();

            descriptor
                .Field(u => u.LockoutEnd)
                .Ignore();

            descriptor
                .Field(u => u.NormalizedEmail)
                .Ignore();

            descriptor
                .Field(u => u.NormalizedUserName)
                .Ignore();

            descriptor
                .Field(u => u.PasswordHash)
                .Ignore();

            descriptor
                .Field(u => u.PhoneNumber)
                .Ignore();

            descriptor
                .Field(u => u.PhoneNumberConfirmed)
                .Ignore();

            descriptor
                .Field(u => u.AccessFailedCount)
                .Ignore();

            descriptor
                .Field(u => u.SecurityStamp)
                .Ignore();

            descriptor
                .Field(u => u.TwoFactorEnabled)
                .Ignore();

            descriptor
                .Field(u => u.Tasks)
                .ResolveWith<Resolvers>(r => r.GetTasks(default, default))
                .UseDbContext<TimeManagerDbContext>()
                .Description("Gets the user's tasks.");
        }

        private class Resolvers
        {
            [UseDbContext(typeof(TimeManagerDbContext))]
            public IQueryable<UserTask> GetTasks(ApplicationUser user, [ScopedService] TimeManagerDbContext context)
            {
                return context.UserTasks.Where(t => t.UserId == user.Id);
            }
        }
    }
}
