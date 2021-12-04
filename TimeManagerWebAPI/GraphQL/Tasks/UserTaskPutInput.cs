using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagerWebAPI.GraphQL.Tasks
{
    public record UserTaskPutInput(string Id, string Name, DateTime Deadline, int TimeEstimation, int Difficulty);
}
