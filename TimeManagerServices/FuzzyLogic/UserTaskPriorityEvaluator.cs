using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using TimeManageData.Models;
using TimeManageData.Repositories;


namespace TimeManagerServices.FuzzyLogic
{
    public static class UserTaskPriorityEvaluator
    {

        public static double TimeEvaluator(UserTask userTask)
        {
            RuleInferenceEngine rie = new RuleInferenceEngine();

            FuzzySet time = new FuzzySet("Time", 1, 9, 0.1);
            time.AddMembership("Low", new FuzzyReverseGrade(1, 4));
            time.AddMembership("Medium", new FuzzyTriangle(3, 5, 7));
            time.AddMembership("High", new FuzzyGrade(6, 9));
            rie.AddFuzzySet(time.Name, time);
            
            FuzzySet difficulty = new FuzzySet("Difficulty", 2, 10, 0.1);
            difficulty.AddMembership("Low", new FuzzyReverseGrade(2, 5));
            difficulty.AddMembership("Medium", new FuzzyTriangle(4, 6, 8));
            difficulty.AddMembership("High", new FuzzyGrade(7,10));
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

            difficulty.X = (userTask.Difficulty + 1) * 2;
            
            rie.Infer(time);

            return time.X;
        }

        public static int ChanceEvaluator(UserTask userTask)
        {
            RuleInferenceEngine rie = new RuleInferenceEngine();

            FuzzySet chance = new FuzzySet("Chance", 1, 100, 0.1);
            chance.AddMembership("Low", new FuzzyReverseGrade(1, 15));
            chance.AddMembership("LowMedium", new FuzzyTriangle(10, 20 ,30));
            chance.AddMembership("Medium", new FuzzyTriangle(25, 50, 75));
            chance.AddMembership("MediumHigh", new FuzzyTriangle(70, 80, 90));
            chance.AddMembership("High", new FuzzyGrade(85, 100));
            rie.AddFuzzySet(chance.Name, chance);
            
            FuzzySet terminate = new FuzzySet("Terminate", 0, 60*24*10, 100);
            terminate.AddMembership("High", new FuzzyReverseGrade(0, 60*24*4));
            terminate.AddMembership("Medium", new FuzzyTriangle(60*24*3, 60*24*5, 60*24*7));
            terminate.AddMembership("Low", new FuzzyGrade(60*24*6, 60*24*10));
            rie.AddFuzzySet(terminate.Name, terminate);
            
            FuzzySet difficulty = new FuzzySet("Difficulty", 2, 10, 0.1);
            difficulty.AddMembership("Low", new FuzzyReverseGrade(2, 5));
            difficulty.AddMembership("Medium", new FuzzyTriangle(4, 6, 8));
            difficulty.AddMembership("High", new FuzzyGrade(7, 10));
            rie.AddFuzzySet(difficulty.Name, difficulty);

            Rule rule = new Rule("Rule 1");
            rule.AddAntecedent(new Clause(terminate, "Is", "Low"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Low"));
            rule.Consequent = new Clause(chance, "Is", "Low");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 2");
            rule.AddAntecedent(new Clause(terminate, "Is", "Medium"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Low"));
            rule.Consequent = new Clause(chance, "Is", "LowMedium");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 3");
            rule.AddAntecedent(new Clause(terminate, "Is", "High"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Low"));
            rule.Consequent = new Clause(chance, "Is", "Medium");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 4");
            rule.AddAntecedent(new Clause(terminate, "Is", "Low"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Medium"));
            rule.Consequent = new Clause(chance, "Is", "LowMedium");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 5");
            rule.AddAntecedent(new Clause(terminate, "Is", "Medium"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Medium"));
            rule.Consequent = new Clause(chance, "Is", "Medium");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 6");
            rule.AddAntecedent(new Clause(terminate, "Is", "High"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Medium"));
            rule.Consequent = new Clause(chance, "Is", "MediumHigh");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 7");
            rule.AddAntecedent(new Clause(terminate, "Is", "Low"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "High"));
            rule.Consequent = new Clause(chance, "Is", "Medium");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 8");
            rule.AddAntecedent(new Clause(terminate, "Is", "Medium"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "High"));
            rule.Consequent = new Clause(chance, "Is", "MediumHigh");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 9");
            rule.AddAntecedent(new Clause(terminate, "Is", "High"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "High"));
            rule.Consequent = new Clause(chance, "Is", "High");
            rie.AddRule(rule);
            
            terminate.X = CalculateMinutes(userTask.Deadline-DateTime.Now);

            difficulty.X = (userTask.Difficulty + 1)*2;

            rie.Infer(chance);
            
            return (int)chance.X;
        }

        public static double PriorityEvaluator(UserTask userTask)
        {
            RuleInferenceEngine rie = new RuleInferenceEngine();

            FuzzySet priority = new FuzzySet("Priority", 1, 12, 0.1);
            priority.AddMembership("VeryLow", new FuzzyReverseGrade(1, 3));
            priority.AddMembership("Low", new FuzzyTriangle(2.5, 3.5, 4.5));
            priority.AddMembership("LowMedium", new FuzzyTriangle(4, 5, 6));
            priority.AddMembership("Medium", new FuzzyTriangle(5.5, 6.5, 7.5));
            priority.AddMembership("MediumHigh", new FuzzyTriangle(7, 8, 9));
            priority.AddMembership("High", new FuzzyTriangle(8.5, 9.5, 10.5));
            priority.AddMembership("VeryHigh", new FuzzyGrade(10,12));
            rie.AddFuzzySet(priority.Name, priority);

            FuzzySet terminate = new FuzzySet("Terminate", 0, 60*24*10, 100);
            terminate.AddMembership("High", new FuzzyReverseGrade(0, 60*24*4));
            terminate.AddMembership("Medium", new FuzzyTriangle(60*24*3, 60*24*5, 60*24*7));
            terminate.AddMembership("Low", new FuzzyGrade(60*24*6, 60*24*10));
            rie.AddFuzzySet(terminate.Name, terminate);
            
            FuzzySet difficulty = new FuzzySet("Difficulty", 2, 10, 0.1);
            difficulty.AddMembership("Low", new FuzzyReverseGrade(2, 5));
            difficulty.AddMembership("Medium", new FuzzyTriangle(4, 6, 8));
            difficulty.AddMembership("High", new FuzzyGrade(7, 10));
            rie.AddFuzzySet(difficulty.Name, difficulty);
            
            FuzzySet importance = new FuzzySet("Importance", 2, 10, 0.1);
            importance.AddMembership("Low", new FuzzyReverseGrade(2, 5));
            importance.AddMembership("Medium", new FuzzyTriangle(4, 6, 8));
            importance.AddMembership("High", new FuzzyGrade(7,10));
            rie.AddFuzzySet(importance.Name, importance);

            Rule rule = new Rule("Rule 1");
            rule.AddAntecedent(new Clause(terminate, "Is", "Low"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Low"));
            rule.AddAntecedent(new Clause(importance, "Is", "Low"));
            rule.Consequent = new Clause(priority, "Is", "VeryLow");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 2");
            rule.AddAntecedent(new Clause(terminate, "Is", "Medium"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Low"));
            rule.AddAntecedent(new Clause(importance, "Is", "Low"));
            rule.Consequent = new Clause(priority, "Is", "Medium");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 3");
            rule.AddAntecedent(new Clause(terminate, "Is", "High"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Low"));
            rule.AddAntecedent(new Clause(importance, "Is", "Low"));
            rule.Consequent = new Clause(priority, "Is", "High");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 4");
            rule.AddAntecedent(new Clause(terminate, "Is", "Low"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Medium"));
            rule.AddAntecedent(new Clause(importance, "Is", "Low"));
            rule.Consequent = new Clause(priority, "Is", "LowMedium");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 5");
            rule.AddAntecedent(new Clause(terminate, "Is", "Low"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "High"));
            rule.AddAntecedent(new Clause(importance, "Is", "Low"));
            rule.Consequent = new Clause(priority, "Is", "MediumHigh");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 6");
            rule.AddAntecedent(new Clause(terminate, "Is", "Medium"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Medium"));
            rule.AddAntecedent(new Clause(importance, "Is", "Low"));
            rule.Consequent = new Clause(priority, "Is", "MediumHigh");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 7");
            rule.AddAntecedent(new Clause(terminate, "Is", "Medium"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "High"));
            rule.AddAntecedent(new Clause(importance, "Is", "Low"));
            rule.Consequent = new Clause(priority, "Is", "MediumHigh");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 8");
            rule.AddAntecedent(new Clause(terminate, "Is", "High"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Medium"));
            rule.AddAntecedent(new Clause(importance, "Is", "Low"));
            rule.Consequent = new Clause(priority, "Is", "High");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 9");
            rule.AddAntecedent(new Clause(terminate, "Is", "High"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "High"));
            rule.AddAntecedent(new Clause(importance, "Is", "Low"));
            rule.Consequent = new Clause(priority, "Is", "High");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 10");
            rule.AddAntecedent(new Clause(terminate, "Is", "Low"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Low"));
            rule.AddAntecedent(new Clause(importance, "Is", "Medium"));
            rule.Consequent = new Clause(priority, "Is", "Medium");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 11");
            rule.AddAntecedent(new Clause(terminate, "Is", "Medium"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Low"));
            rule.AddAntecedent(new Clause(importance, "Is", "Medium"));
            rule.Consequent = new Clause(priority, "Is", "MediumHigh");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 12");
            rule.AddAntecedent(new Clause(terminate, "Is", "High"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Low"));
            rule.AddAntecedent(new Clause(importance, "Is", "Medium"));
            rule.Consequent = new Clause(priority, "Is", "High");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 13");
            rule.AddAntecedent(new Clause(terminate, "Is", "Low"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Medium"));
            rule.AddAntecedent(new Clause(importance, "Is", "Medium"));
            rule.Consequent = new Clause(priority, "Is", "MediumHigh");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 14");
            rule.AddAntecedent(new Clause(terminate, "Is", "Low"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "High"));
            rule.AddAntecedent(new Clause(importance, "Is", "Medium"));
            rule.Consequent = new Clause(priority, "Is", "MediumHigh");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 15");
            rule.AddAntecedent(new Clause(terminate, "Is", "Medium"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Medium"));
            rule.AddAntecedent(new Clause(importance, "Is", "Medium"));
            rule.Consequent = new Clause(priority, "Is", "MediumHigh");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 16");
            rule.AddAntecedent(new Clause(terminate, "Is", "Medium"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "High"));
            rule.AddAntecedent(new Clause(importance, "Is", "Medium"));
            rule.Consequent = new Clause(priority, "Is", "High");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 17");
            rule.AddAntecedent(new Clause(terminate, "Is", "High"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Medium"));
            rule.AddAntecedent(new Clause(importance, "Is", "Medium"));
            rule.Consequent = new Clause(priority, "Is", "High");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 18");
            rule.AddAntecedent(new Clause(terminate, "Is", "High"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "High"));
            rule.AddAntecedent(new Clause(importance, "Is", "Medium"));
            rule.Consequent = new Clause(priority, "Is", "VeryHigh");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 19");
            rule.AddAntecedent(new Clause(terminate, "Is", "Low"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Low"));
            rule.AddAntecedent(new Clause(importance, "Is", "High"));
            rule.Consequent = new Clause(priority, "Is", "High");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 20");
            rule.AddAntecedent(new Clause(terminate, "Is", "Medium"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Low"));
            rule.AddAntecedent(new Clause(importance, "Is", "High"));
            rule.Consequent = new Clause(priority, "Is", "MediumHigh");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 21");
            rule.AddAntecedent(new Clause(terminate, "Is", "High"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Low"));
            rule.AddAntecedent(new Clause(importance, "Is", "High"));
            rule.Consequent = new Clause(priority, "Is", "VeryHigh");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 22");
            rule.AddAntecedent(new Clause(terminate, "Is", "Low"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Medium"));
            rule.AddAntecedent(new Clause(importance, "Is", "High"));
            rule.Consequent = new Clause(priority, "Is", "High");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 23");
            rule.AddAntecedent(new Clause(terminate, "Is", "Low"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "High"));
            rule.AddAntecedent(new Clause(importance, "Is", "High"));
            rule.Consequent = new Clause(priority, "Is", "High");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 24");
            rule.AddAntecedent(new Clause(terminate, "Is", "Medium"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Medium"));
            rule.AddAntecedent(new Clause(importance, "Is", "High"));
            rule.Consequent = new Clause(priority, "Is", "High");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 25");
            rule.AddAntecedent(new Clause(terminate, "Is", "Medium"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "High"));
            rule.AddAntecedent(new Clause(importance, "Is", "High"));
            rule.Consequent = new Clause(priority, "Is", "VeryHigh");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 26");
            rule.AddAntecedent(new Clause(terminate, "Is", "High"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "Medium"));
            rule.AddAntecedent(new Clause(importance, "Is", "High"));
            rule.Consequent = new Clause(priority, "Is", "VeryHigh");
            rie.AddRule(rule);
            
            rule = new Rule("Rule 27");
            rule.AddAntecedent(new Clause(terminate, "Is", "High"));
            rule.AddAntecedent(new Clause(difficulty, "Is", "High"));
            rule.AddAntecedent(new Clause(importance, "Is", "High"));
            rule.Consequent = new Clause(priority, "Is", "VeryHigh");
            rie.AddRule(rule);

            terminate.X = CalculateMinutes(userTask.Deadline-DateTime.Now);

            difficulty.X = (userTask.Difficulty + 1)*2;

            importance.X = (userTask.Importance+1)*2;

            rie.Infer(priority);
            
            return priority.X;
        }
        
        /*public static double EstimatePriority(UserTask userTask)
        {
            return 1/((userTask.Difficulty+1)*Math.Pow(CalculateMin(userTask.Deadline-DateTime.Now),2)*userTask.TotalSeconds/60);
        }*/

        private static int CalculateMinutes(TimeSpan date)
        {
            return (int)date.TotalMinutes;
        }
    }
}