﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using TimeManageData.DbContexts;
using TimeManageData.Models;
using TimeManageData.Repositories;
using TimeManagerWebAPI.GraphQL.Tasks;

namespace TimeManagerWebAPI.GraphQL
{
    [GraphQLDescription("Represents user & task queries.")]
    public class Query
    {
        [UseFiltering]
        [UseSorting]
        [GraphQLDescription("Represents the query for retrieving user's tasks.")]
        public IQueryable<UserTask> GetTask([Service] IRepository<UserTask> taskRepo)
        {
            return taskRepo.GetAll().AsQueryable();
        }
    }
}
