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
using TimeManagerWebAPI.Extensions;
using TimeManagerWebAPI.GraphQL.Tasks;
using TimeManagerWebAPI.Validators;

namespace TimeManagerWebAPI.GraphQL
{
    [GraphQLDescription("Represents task mutations.")]
    public class Mutation
    {
        [Authorize(Policy = "Auth")]
        public async Task<UserTaskPutPayload> PutUserTask(
            UserTaskPutInput input,
            [Service] IRepository<UserTask> taskRepo,
            [Service] IHttpContextAccessor contextAccessor)
        {
            UserTaskPutInputValidator validator = new();
            await validator.ValidateAndThrowGraphQLExceptionAsync(input);

            string userId = contextAccessor.HttpContext!.User.Claims.First().Value;

            UserTask taskToPut = taskRepo.GetAll().FirstOrDefault(t => t.Id == input?.Id);

            if (taskToPut is null || taskToPut.UserId != userId)
            {
                throw new GraphQLException(ErrorMessages.CantUpdateUserTask);
            }

            taskToPut.Name = input.Name;
            taskToPut.Deadline = input.Deadline;
            taskToPut.Importance = input.Importance;
            taskToPut.Difficulty = input.Difficulty;

            taskToPut.PriorityEvaluation = UserTaskPriorityEvaluator.PriorityEvaluator(taskToPut);
            taskToPut.TimeEvaluation = UserTaskPriorityEvaluator.TimeEvaluator(taskToPut);
            taskToPut.DeadlineMissEvaluation = UserTaskPriorityEvaluator.ChanceEvaluator(taskToPut);

            await taskRepo.SaveChangesAsync().ConfigureAwait(false);

            return new UserTaskPutPayload(taskToPut);
        }

        [Authorize(Policy = "Auth")]
        public async Task<UserTaskDeletePayload> DeleteUserTask(
            UserTaskDeleteInput input,
            [Service] IRepository<UserTask> userRepo,
            [Service] IHttpContextAccessor contextAccessor)
        {
            UserTaskDeleteInputValidator validator = new();
            await validator.ValidateAndThrowGraphQLExceptionAsync(input);

            string userId = contextAccessor.HttpContext!.User.Claims.First().Value;

            UserTask taskToDelete = userRepo.GetAll().FirstOrDefault(t => t.Id == input.Id);

            if (taskToDelete is null || taskToDelete.UserId != userId)
            {
                throw new GraphQLException(ErrorMessages.CantDeleteUserTask);
            }

            UserTask deletedTask = userRepo.Delete(taskToDelete.Id);

            deletedTask.PriorityEvaluation = UserTaskPriorityEvaluator.PriorityEvaluator(deletedTask);
            deletedTask.TimeEvaluation = UserTaskPriorityEvaluator.TimeEvaluator(deletedTask);
            deletedTask.DeadlineMissEvaluation = UserTaskPriorityEvaluator.ChanceEvaluator(deletedTask);

            await userRepo.SaveChangesAsync();

            return new UserTaskDeletePayload(deletedTask);
        }

        [Authorize(Policy = "Auth")]
        public async Task<UserTaskAddPayload> AddUserTask(
            UserTaskAddInput input,
            [Service] IRepository<UserTask> taskRepo,
            [Service] IHttpContextAccessor contextAccessor)
        {
            UserTaskAddInputValidator validator = new();
            await validator.ValidateAndThrowGraphQLExceptionAsync(input);

            string userId = contextAccessor.HttpContext!.User.Claims.First().Value;

            UserTask newTask = new()
            {
                Id = Guid.NewGuid().ToString(),
                Deadline = input.Deadline,
                Name = input.Name,
                Importance = input.Importance,
                Difficulty = input.Difficulty,
                UserId = userId
            };

            taskRepo.Create(newTask);

            newTask.PriorityEvaluation = UserTaskPriorityEvaluator.PriorityEvaluator(newTask);
            newTask.TimeEvaluation = UserTaskPriorityEvaluator.TimeEvaluator(newTask);
            newTask.DeadlineMissEvaluation = UserTaskPriorityEvaluator.ChanceEvaluator(newTask);

            await taskRepo.SaveChangesAsync();

            return new UserTaskAddPayload(newTask);
        }
    }
}
