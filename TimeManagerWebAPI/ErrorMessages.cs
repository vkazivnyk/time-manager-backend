using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagerWebAPI
{
    public static class ErrorMessages
    {
        public const string UserTaskIdEmpty = "User task id can not be empty.";

        public const string UserTaskNameEmpty = "User task name can not be empty.";

        public const string UserTaskDeadlineEmpty = "User task deadline can not be empty.";

        public const string UserTaskDifficultyEmpty = "User task difficulty can not be empty.";

        public const string UserTaskTotalSecondsEmpty = "User task time span can not be empty.";

        public const string CantUpdateUserTask = "There was a problem updating a user task.";

        public const string CantDeleteUserTask = "There was a problem deleting a user task.";
    }
}
