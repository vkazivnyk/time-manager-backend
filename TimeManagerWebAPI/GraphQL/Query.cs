using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using TimeManageData.DbContexts;
using TimeManageData.Models;
using TimeManageData.Repositories;

namespace TimeManagerWebAPI.GraphQL
{
    [GraphQLDescription("Represents user & task queries.")]
    public class Query
    {
        [GraphQLDescription("Represents the query for retrieving user's tasks.")]
        public IQueryable<UserTask> GetTask([Service] UserTaskMockRepository taskRepo)
        {
            return taskRepo.GetAll().AsQueryable();
        }
    }
}
