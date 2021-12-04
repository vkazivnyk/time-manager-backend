using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagerWebAPI.GraphQL.Tasks
{
    public record UserTaskAddInput(string Name, DateTime Deadline, int Importance, int Difficulty);
}
