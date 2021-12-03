using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using TimeManageData.DbContexts;
using TimeManageData.Enums;
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
            taskToPut.Difficulty = (Difficulty)input.Difficulty;

            await context.SaveChangesAsync().ConfigureAwait(false);

            return new UserTaskPutPayload(taskToPut);
        }

        [UseDbContext(typeof(TimeManagerDbContext))]
        public async Task<UserTaskDeletePayload> DeleteUserTask(UserTaskDeleteInput input, [ScopedService] TimeManagerDbContext context)
        {
            UserTask taskToDelete = context.UserTasks.FirstOrDefault(t => t.Id == input.Id);

            if (taskToDelete is null)
            {
                throw new GraphQLException("There was no task with the specified id.");
            }

            context.UserTasks.Remove(taskToDelete);

            await context.SaveChangesAsync();

            return new UserTaskDeletePayload(taskToDelete);
        }
    }
}
