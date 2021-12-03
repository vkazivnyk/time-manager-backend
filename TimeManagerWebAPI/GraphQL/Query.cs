using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using TimeManageData.DbContexts;

namespace TimeManagerWebAPI.GraphQL
{
    [GraphQLDescription("Represents user & task queries.")]
    public class Query
    {
        [UseDbContext(typeof(TimeManagerDbContext))]
        public IQueryable<TimeManageData.Models.Task> GetTask([ScopedService] TimeManagerDbContext context)
        {
            return context.Tasks.AsQueryable();
        }
    }
}
