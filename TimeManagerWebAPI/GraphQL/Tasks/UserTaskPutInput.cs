using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeManageData.Enums;

namespace TimeManagerWebAPI.GraphQL.Tasks
{
    public record UserTaskPutInput(string Id, string Name, DateTime Deadline, long TotalSeconds, int Difficulty);
}
