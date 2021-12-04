using System;
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
    [GraphQLDescription("Represents task mutations.")]
    public class Mutation
    {
        public async Task<UserTaskPutPayload> PutUserTask(UserTaskPutInput input, [Service] UserTaskMockRepository taskRepo)
        {
            UserTask taskToPut = taskRepo.GetAll().FirstOrDefault(t => t.Id == input.Id);

            taskToPut.Name = input.Name;
            taskToPut.Deadline = input.Deadline;
            taskToPut.TotalSeconds = input.TotalSeconds;
            taskToPut.Difficulty = input.Difficulty;

            await taskRepo.SaveChangesAsync().ConfigureAwait(false);

            return new UserTaskPutPayload(taskToPut);
        }
    }
}
