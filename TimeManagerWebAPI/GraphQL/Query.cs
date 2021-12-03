using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using TimeManageData.DbContexts;
using TimeManageData.Models;

namespace TimeManagerWebAPI.GraphQL
{
    [GraphQLDescription("Represents user & task queries.")]
    public class Query
    {
        [UseDbContext(typeof(TimeManagerDbContext))]
        [GraphQLDescription("Represents the query for retrieving user's tasks.")]
        public IQueryable<UserTask> GetTask([ScopedService] TimeManagerDbContext context)
        {
            return context.UserTasks.AsQueryable();
        }
    }
}
