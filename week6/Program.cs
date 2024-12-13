using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Goal> goals = new List<Goal>();
            int totalScore = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Score: {totalScore}");
                Console.WriteLine("1. Create New Goal");
                Console.WriteLine("2. Record Event");
                Console.WriteLine("3. Display Goals");
                Console.WriteLine("4. Save Goals");
                Console.WriteLine("5. Load Goals");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateGoal(goals);
                        break;
                    case "2":
                        totalScore += RecordEvent(goals);
                        break;
                    case "3":
                        DisplayGoals(goals);
                        break;
                    case "4":
                        SaveGoals(goals, totalScore);
                        break;
                    case "5":
                        totalScore = LoadGoals(goals);
                        break;
                    case "6":
                        return;
                }
            }
        }

        static void CreateGoal(List<Goal> goals)
        {
            Console.WriteLine("1. Simple Goal");
            Console.WriteLine("2. Eternal Goal");
            Console.WriteLine("3. Checklist Goal");
            Console.Write("Choose a goal type: ");
            string choice = Console.ReadLine();

            Console.Write("Enter goal title: ");
            string title = Console.ReadLine();

            Console.Write("Enter goal description: ");
            string description = Console.ReadLine();

            Console.Write("Enter goal points: ");
            int points = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case "1":
                    goals.Add(new SimpleGoal(title, description, points));
                    break;
                case "2":
                    goals.Add(new EternalGoal(title, description, points));
                    break;
                case "3":
                    Console.Write("Enter target count: ");
                    int targetCount = int.Parse(Console.ReadLine());
                    Console.Write("Enter bonus points: ");
                    int bonusPoints = int.Parse(Console.ReadLine());
                    goals.Add(new ChecklistGoal(title, description, points, targetCount, bonusPoints));
                    break;
            }
        }

        static int RecordEvent(List<Goal> goals)
        {
            Console.WriteLine("Choose a goal to record progress:");
            for (int i = 0; i < goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {goals[i].GetDetailsString()}");
            }
            int choice = int.Parse(Console.ReadLine()) - 1;

            return goals[choice].RecordEvent();
        }

        static void DisplayGoals(List<Goal> goals)
        {
            foreach (var goal in goals)
            {
                Console.WriteLine(goal.GetDetailsString());
            }
            Console.ReadLine();
        }

        static void SaveGoals(List<Goal> goals, int score)
        {
            using (StreamWriter writer = new StreamWriter("goals.txt"))
            {
                writer.WriteLine(score);
                foreach (var goal in goals)
                {
                    writer.WriteLine(goal.ToString());
                }
            }
        }

        static int LoadGoals(List<Goal> goals)
        {
            goals.Clear();
            using (StreamReader reader = new StreamReader("goals.txt"))
            {
                int score = int.Parse(reader.ReadLine());
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    goals.Add(Goal.Parse(line));
                }
                return score;
            }
        }
    }

    abstract class Goal
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }

        public Goal(string title, string description, int points)
        {
            Title = title;
            Description = description;
            Points = points;
        }

        public abstract int RecordEvent();
        public virtual string GetDetailsString() => $"{Title}: {Description}";
        public abstract override string ToString();
        public static Goal Parse(string data)
        {
            // Logic to parse string data back into a Goal object.
        }
    }

    class SimpleGoal : Goal
    {
        private bool _isComplete;

        public SimpleGoal(string title, string description, int points)
            : base(title, description, points)
        {
            _isComplete = false;
        }

        public override int RecordEvent()
        {
            if (!_isComplete)
            {
                _isComplete = true;
                return Points;
            }
            return 0;
        }

        public override string GetDetailsString()
            => _isComplete ? $"[X] {base.GetDetailsString()}" : $"[ ] {base.GetDetailsString()}";

        public override string ToString()
            => $"SimpleGoal|{Title}|{Description}|{Points}|{_isComplete}";
    }

    class EternalGoal : Goal
    {
        public EternalGoal(string title, string description, int points)
            : base(title, description, points) { }

        public override int RecordEvent() => Points;

        public override string ToString()
            => $"EternalGoal|{Title}|{Description}|{Points}";
    }

    class ChecklistGoal : Goal
    {
        private int _currentCount;
        private int _targetCount;
        private int _bonusPoints;

        public ChecklistGoal(string title, string description, int points, int targetCount, int bonusPoints)
            : base(title, description, points)
        {
            _currentCount = 0;
            _targetCount = targetCount;
            _bonusPoints = bonusPoints;
        }

        public override int RecordEvent()
        {
            _currentCount++;
            if (_currentCount == _targetCount)
            {
                return Points + _bonusPoints;
            }
            return Points;
        }

        public override string GetDetailsString()
            => $"[ ] {base.GetDetailsString()} (Completed {_currentCount}/{_targetCount})";

        public override string ToString()
            => $"ChecklistGoal|{Title}|{Description}|{Points}|{_currentCount}|{_targetCount}|{_bonusPoints}";
    }
}
