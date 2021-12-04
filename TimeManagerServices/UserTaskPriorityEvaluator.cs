using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using TimeManageData.Models;
using TimeManageData.Repositories;


namespace TimeManagerServices
{
    public static class UserTaskPriorityEvaluator
    {

        public static double TimeEvaluator(UserTask userTask)
        {
            RuleInferenceEngine rie = new RuleInferenceEngine();

            FuzzySet time = new FuzzySet("Time", 1, 9, 0.1);
            time.AddMembership("Low", new FuzzyTriangle(1, 1, 4));
            time.AddMembership("Medium", new FuzzyTriangle(3, 5, 7));
            time.AddMembership("High", new FuzzyTriangle(6, 9, 9));
            rie.AddFuzzySet(time.Name, time);
            
            FuzzySet difficulty = new FuzzySet("Difficulty", 2, 10, 0.1);
            difficulty.AddMembership("Low", new FuzzyTriangle(2, 2, 5));
            difficulty.AddMembership("Medium", new FuzzyTriangle(4, 6, 8));
            difficulty.AddMembership("High", new FuzzyTriangle(7,10, 10));
            rie.AddFuzzySet(difficulty.Name, difficulty);
            
            Rule rule = new Rule("Rule 1");
            rule.AddAntecedent(new Clause(difficulty, "Is", "High"));
            rule.Consequent = new Clause(time, "Is", "High");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 2");
            rule.AddAntecedent(new Clause(difficulty, "Is", "Medium"));
            rule.Consequent = new Clause(time, "Is", "Medium");
            rie.AddRule(rule);

            rule = new Rule("Rule 3");
            rule.AddAntecedent(new Clause(difficulty, "Is", "Low"));
            rule.Consequent = new Clause(time, "Is", "Low");
            rie.AddRule(rule);

            difficulty.X = userTask.Difficulty + 1;
            
            rie.Infer(time);

            return time.X;
        }

        public static double PriorityEvaluator(UserTask userTask)
        {
            RuleInferenceEngine rie = new RuleInferenceEngine();

            FuzzySet priority = new FuzzySet("Priority", 1, 9, 0.1);
            priority.AddMembership("Low", new FuzzyTriangle(1, 1, 4));
            priority.AddMembership("Medium", new FuzzyTriangle(3, 5, 7));
            priority.AddMembership("High", new FuzzyTriangle(6, 9, 9));
            rie.AddFuzzySet(priority.Name, priority);

            FuzzySet terminate = new FuzzySet("Terminate", 1, 30, 0.1);
            terminate.AddMembership("High", new FuzzyTriangle(0, 0, 12));
            terminate.AddMembership("Medium", new FuzzyTriangle(10, 15, 20));
            terminate.AddMembership("Low", new FuzzyTriangle(22, 30, 30));
            rie.AddFuzzySet(terminate.Name, terminate);
            
            FuzzySet difficulty = new FuzzySet("Difficulty", 2, 10, 0.1);
            difficulty.AddMembership("Low", new FuzzyTriangle(2, 2, 5));
            difficulty.AddMembership("Medium", new FuzzyTriangle(4, 6, 8));
            difficulty.AddMembership("High", new FuzzyTriangle(7,10, 10));
            rie.AddFuzzySet(difficulty.Name, difficulty);
            
            FuzzySet importance = new FuzzySet("Importance", 2, 10, 0.1);
            importance.AddMembership("Low", new FuzzyTriangle(2, 2, 5));
            importance.AddMembership("Medium", new FuzzyTriangle(4, 6, 8));
            importance.AddMembership("High", new FuzzyTriangle(7,10, 10));
            rie.AddFuzzySet(importance.Name, importance);

            Rule rule = new Rule("Rule 1");
            rule.AddAntecedent(new Clause(terminate, "Is", "Low"));
            rule.Consequent = new Clause(priority, "Is", "High");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 2");
            rule.AddAntecedent(new Clause(terminate, "Is", "Medium"));
            rule.Consequent = new Clause(priority, "Is", "High");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 3");
            rule.AddAntecedent(new Clause(terminate, "Is", "High"));
            rule.Consequent = new Clause(priority, "Is", "Medium");
            rie.AddRule(rule);

            rule = new Rule("Rule 4");
            rule.AddAntecedent(new Clause(difficulty, "Is", "High"));
            rule.Consequent = new Clause(priority, "Is", "High");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 5");
            rule.AddAntecedent(new Clause(difficulty, "Is", "Medium"));
            rule.Consequent = new Clause(priority, "Is", "Medium");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 6");
            rule.AddAntecedent(new Clause(difficulty, "Is", "Low"));
            rule.Consequent = new Clause(priority, "Is", "Low");
            rie.AddRule(rule);

            rule = new Rule("Rule 7");
            rule.AddAntecedent(new Clause(importance, "Is", "Medium"));
            rule.Consequent = new Clause(priority, "Is", "High");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 8");
            rule.AddAntecedent(new Clause(importance, "Is", "High"));
            rule.Consequent = new Clause(priority, "Is", "High");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 9");
            rule.AddAntecedent(new Clause(importance, "Is", "Low"));
            rule.Consequent = new Clause(priority, "Is", "Medium");
            rie.AddRule(rule);
            
            

            terminate.X = CalculateDay(userTask.Deadline-DateTime.Now);

            difficulty.X = userTask.Difficulty + 1;
            
            

            rie.Infer(priority);
            
            return priority.X;
        }
        
        /*public static double EstimatePriority(UserTask userTask)
        {
            return 1/((userTask.Difficulty+1)*Math.Pow(CalculateMin(userTask.Deadline-DateTime.Now),2)*userTask.TotalSeconds/60);
        }*/

        private static int CalculateDay(TimeSpan date)
        {
            return date.Days;
        }
    }
}