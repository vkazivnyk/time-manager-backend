using System;
using System.Collections.Generic;
using TimeManageData.Repositories;


namespace TimeManagerServices
{
    public class UserTaskPriorityEvaluator
    {
        private UserTaskMockRepository _userTaskRepository;
        
        public UserTaskPriorityEvaluator(UserTaskMockRepository userTask)
        {
            _userTaskRepository = userTask;
        }
        
        public double EstimatePriority()
        {
            //return (double)1/(complexity*CalculateMin(deadLine-DateTime.Now)*leadTime);
            return 0;
        }

        public int CalculateMin(TimeSpan date)
        {
            return date.Days * 24 * 60 + date.Hours * 60 +
                   date.Minutes;
        }
    }
}