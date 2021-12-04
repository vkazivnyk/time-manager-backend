using System;
using System.Collections.Generic;
using TimeManageData.Models;
using TimeManageData.Repositories;


namespace TimeManagerServices
{
    public static class UserTaskPriorityEvaluator
    {
        public static double EstimatePriority(UserTask userTask)
        {
            return 1/(userTask.Difficulty*Math.Pow(CalculateMin(userTask.Deadline-DateTime.Now),2)*userTask.TotalSeconds/60);
        }

        private static int CalculateMin(TimeSpan date)
        {
            return date.Days * 24 * 60 + date.Hours * 60 +
                   date.Minutes;
        }

        private static void MembershipFunction()
        {
            
        }
    }
}