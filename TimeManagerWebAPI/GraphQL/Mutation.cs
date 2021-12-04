using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using TimeManageData.DbContexts;
using TimeManageData.Models;
using TimeManageData.Repositories;
using TimeManagerWebAPI.Extensions;
using TimeManagerWebAPI.GraphQL.Tasks;
using TimeManagerWebAPI.Validators;

namespace TimeManagerWebAPI.GraphQL
{
    [GraphQLDescription("Represents task mutations.")]
    public class Mutation
    {
        public async Task<UserTaskPutPayload> PutUserTask(
            UserTaskPutInput input,
            [Service] UserTaskMockRepository taskRepo)
        {
            UserTask taskToPut = taskRepo.GetAll().FirstOrDefault(t => t.Id == input?.Id);

            if (taskToPut is null)
            {
                throw new GraphQLException(ErrorMessages.CantUpdateUserTask);
            }

            UserTaskPutInputValidator validator = new();
            await validator.ValidateAndThrowGraphQLExceptionAsync(input);

            taskToPut.Name = input.Name;
            taskToPut.Deadline = input.Deadline;
            taskToPut.TotalSeconds = input.TotalSeconds;
            taskToPut.Difficulty = input.Difficulty;

            await taskRepo.SaveChangesAsync().ConfigureAwait(false);

            return new UserTaskPutPayload(taskToPut);
        }

        public async Task<UserTaskDeletePayload> DeleteUserTask(
            UserTaskDeleteInput input,
            [Service] UserTaskMockRepository userRepo)
        {
            UserTask taskToDelete = userRepo.GetAll().FirstOrDefault(t => t.Id == input.Id);

            if (taskToDelete is null)
            {
                throw new GraphQLException(ErrorMessages.CantDeleteUserTask);
            }

            UserTaskDeleteInputValidator validator = new();
            await validator.ValidateAndThrowGraphQLExceptionAsync(input);

            UserTask deletedTask = userRepo.Delete(taskToDelete.Id);

            await userRepo.SaveChangesAsync();

            return new UserTaskDeletePayload(deletedTask);
        }

        public async Task<UserTaskAddPayload> AddUserTask(
            UserTaskAddInput input,
            [Service] UserTaskMockRepository taskRepo,
            [Service] UserMockRepository userRepo)
        {
            UserTaskAddInputValidator validator = new();
            await validator.ValidateAndThrowGraphQLExceptionAsync(input);

            UserTask newTask = new()
            {
                Id = Guid.NewGuid().ToString(),
                User = userRepo.Find("afdfauiewhfkj"),
                Deadline = input.Deadline,
                Name = input.Name,
                TotalSeconds = input.TotalSeconds,
                Difficulty = input.Difficulty,
                UserId = "afdfauiewhfkj"
            };

            taskRepo.Create(newTask);

            await taskRepo.SaveChangesAsync();

            return new UserTaskAddPayload(newTask);
        }
    }
}
