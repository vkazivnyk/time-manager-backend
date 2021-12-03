using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using TimeManageData.DbContexts;
using TimeManageData.Models;
using TimeManagerWebAPI.GraphQL.Tasks;

namespace TimeManagerWebAPI.GraphQL
{
    [GraphQLDescription("Represents task mutations.")]
    public class Mutation
    {
        [UseDbContext(typeof(TimeManagerDbContext))]
        public async Task<UserTaskPutPayload> PutUserTask(UserTaskPutInput input, [ScopedService] TimeManagerDbContext context)
        {
            UserTask taskToPut = context.UserTasks.FirstOrDefault(t => t.Id == input.Id);

            taskToPut.Name = input.Name;
            taskToPut.Deadline = input.Deadline;
            taskToPut.TotalSeconds = input.TotalSeconds;
            taskToPut.Difficulty = input.Difficulty;

            await context.SaveChangesAsync().ConfigureAwait(false);

            return new UserTaskPutPayload(taskToPut);
        }
    }
}
