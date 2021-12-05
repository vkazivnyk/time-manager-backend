using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using Microsoft.AspNetCore.Http;
using TimeManageData.DbContexts;
using TimeManageData.Models;
using TimeManageData.Repositories;
using TimeManagerServices.FuzzyLogic;
using TimeManagerWebAPI.GraphQL.Tasks;

namespace TimeManagerWebAPI.GraphQL
{
    [GraphQLDescription("Represents user & task queries.")]
    public class Query
    {
        [UseFiltering]
        [UseSorting]
        [GraphQLDescription("Represents the query for retrieving user's tasks.")]
        [Authorize(Policy = "Auth")]
        public IQueryable<UserTask> GetTask(
            [Service] IRepository<UserTask> taskRepo,
            [Service] IHttpContextAccessor contextAccessor)
        {
            string userId = contextAccessor.HttpContext!.User.Claims.First().Value;

            List<UserTask> allTasks = taskRepo.Where(t => t.UserId == userId);

            foreach (UserTask task in allTasks)
            {
                task.TimeEvaluation = UserTaskPriorityEvaluator.TimeEvaluator(task);
                task.PriorityEvaluation = UserTaskPriorityEvaluator.PriorityEvaluator(task);
                //TODO: Replace with call
                task.DeadlineMissEvaluation = 50;
            }

            return allTasks.AsQueryable();
        }
    }
}
