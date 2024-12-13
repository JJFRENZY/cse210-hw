using System;
using System.Collections.Generic;
using System.IO;

public abstract class Goal
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }
    public bool IsCompleted { get; set; }

    public Goal(string name, string description, int points)
    {
        Name = name;
        Description = description;
        Points = points;
        IsCompleted = false;
    }

    // Abstract method to record events specific to the goal
    public abstract void RecordEvent();

    // Method to get details of the goal
    public virtual string GetDetailsString()
    {
        return $"{Name}: {Description} - {Points} points";
    }
}

public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points)
        : base(name, description, points)
    { }

    public override void RecordEvent()
    {
        if (!IsCompleted)
        {
            IsCompleted = true;
            Console.WriteLine($"{Name} is now complete! You've earned {Points} points.");
        }
    }

    public override string GetDetailsString()
    {
        return base.GetDetailsString() + (IsCompleted ? " [X]" : " [ ]");
    }
}

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    { }

    public override void RecordEvent()
    {
        if (!IsCompleted)
        {
            Console.WriteLine($"You've earned {Points} points for completing the goal {Name}.");
        }
    }
}

public class ChecklistGoal : Goal
{
    public int TotalTimes { get; set; }
    public int CompletedTimes { get; set; }
    public int BonusPoints { get; set; }

    public ChecklistGoal(string name, string description, int points, int totalTimes, int bonusPoints)
        : base(name, description, points)
    {
        TotalTimes = totalTimes;
        CompletedTimes = 0;
        BonusPoints = bonusPoints;
    }

    public override void RecordEvent()
    {
        if (CompletedTimes < TotalTimes)
        {
            CompletedTimes++;
            Console.WriteLine($"You've completed {CompletedTimes}/{TotalTimes} for {Name}. You've earned {Points} points.");
            if (CompletedTimes == TotalTimes)
            {
                Console.WriteLine($"Bonus! You've earned {BonusPoints} additional points for completing all steps of {Name}.");
            }
        }
    }

    public override string GetDetailsString()
    {
        return $"{base.GetDetailsString()} - Completed {CompletedTimes}/{TotalTimes} times";
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        List<Goal> goals = new List<Goal>();

        // Adding goals
        goals.Add(new SimpleGoal("Run a Marathon", "Complete a marathon", 1000));
        goals.Add(new EternalGoal("Read Scriptures", "Read the scriptures daily", 100));
        goals.Add(new ChecklistGoal("Attend the Temple", "Attend the temple 10 times", 50, 10, 500));

        // Display current goals
        Console.WriteLine("Your Goals:");
        foreach (var goal in goals)
        {
            Console.WriteLine(goal.GetDetailsString());
        }

        // Record events
        Console.WriteLine("\nRecording events...\n");
        goals[0].RecordEvent();  // Completing the "Run a Marathon" goal
        goals[2].RecordEvent();  // Recording one completion for "Attend the Temple"

        // Display updated goals
        Console.WriteLine("\nUpdated Goals after Recording Events:");
        foreach (var goal in goals)
        {
            Console.WriteLine(goal.GetDetailsString());
        }

        // Save and load goals
        SaveGoals(goals);
        List<Goal> loadedGoals = LoadGoals();

        // Display loaded goals
        Console.WriteLine("\nLoaded Goals from file:");
        foreach (var goal in loadedGoals)
        {
            Console.WriteLine(goal.GetDetailsString());
        }
    }

    // Method to save goals to a file
    public static void SaveGoals(List<Goal> goals)
    {
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            foreach (var goal in goals)
            {
                writer.WriteLine($"{goal.GetType().Name},{goal.Name},{goal.Description},{goal.Points},{goal.IsCompleted}");
            }
        }
        Console.WriteLine("\nGoals saved to file.");
    }

    // Method to load goals from a file
    public static List<Goal> LoadGoals()
    {
        List<Goal> goals = new List<Goal>();
        try
        {
            using (StreamReader reader = new StreamReader("goals.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    Goal goal = null;
                    switch (parts[0])
                    {
                        case "SimpleGoal":
                            goal = new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]));
                            break;
                        case "EternalGoal":
                            goal = new EternalGoal(parts[1], parts[2], int.Parse(parts[3]));
                            break;
                        case "ChecklistGoal":
                            goal = new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), 10, 500); // Example
                            break;
                    }
                    goal.IsCompleted = bool.Parse(parts[4]);
                    goals.Add(goal);
                }
            }
            Console.WriteLine("\nGoals loaded from file.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading goals: " + e.Message);
        }

        return goals;
    }
}
