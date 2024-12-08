using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

// Base Class
abstract class MindfulnessActivity
{
    protected string Name { get; set; }
    protected string Description { get; set; }
    protected int Duration { get; set; }

    public void StartActivity()
    {
        Console.Clear();
        Console.WriteLine($"--- {Name} ---");
        Console.WriteLine($"{Description}");
        Console.Write("Enter the duration of this activity (in seconds): ");
        Duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        DisplayAnimation();
    }

    protected void DisplayAnimation()
    {
        for (int i = 0; i < 3; i++)
        {
            Console.Write(".");
            Thread.Sleep(500);
        }
        Console.WriteLine();
    }

    public void EndActivity()
    {
        Console.WriteLine("\nGreat job!");
        Console.WriteLine($"You completed the {Name} for {Duration} seconds.");
        DisplayAnimation();
    }

    public abstract void PerformActivity();
}

// Breathing Activity
class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity()
    {
        Name = "Breathing Activity";
        Description = "This activity will help you relax by guiding you through deep breathing.";
    }

    public override void PerformActivity()
    {
        StartActivity();
        for (int i = 0; i < Duration / 4; i++)
        {
            Console.WriteLine("Breathe in...");
            Countdown(4);
            Console.WriteLine("Breathe out...");
            Countdown(4);
        }
        EndActivity();
    }

    private void Countdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

// Reflection Activity
class ReflectionActivity : MindfulnessActivity
{
    private static readonly List<string> Prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something truly selfless.",
        "Think of a time when you overcame a difficult challenge."
    };

    private static readonly List<string> Questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "What did you learn about yourself?",
        "How can this experience help you in the future?"
    };

    public ReflectionActivity()
    {
        Name = "Reflection Activity";
        Description = "This activity will guide you to reflect on moments of strength and resilience.";
    }

    public override void PerformActivity()
    {
        StartActivity();
        Random random = new Random();
        Console.WriteLine(Prompts[random.Next(Prompts.Count)]);
        DisplayAnimation();

        for (int i = 0; i < Duration / 10; i++)
        {
            Console.WriteLine(Questions[random.Next(Questions.Count)]);
            DisplayAnimation();
        }
        EndActivity();
    }
}

// Listing Activity
class ListingActivity : MindfulnessActivity
{
    private static readonly List<string> Prompts = new List<string>
    {
        "List people you appreciate.",
        "List personal strengths you are proud of.",
        "List moments that brought you joy recently."
    };

    public ListingActivity()
    {
        Name = "Listing Activity";
        Description = "This activity encourages you to focus on positive aspects of your life.";
    }

    public override void PerformActivity()
    {
        StartActivity();
        Random random = new Random();
        Console.WriteLine(Prompts[random.Next(Prompts.Count)]);
        DisplayAnimation();

        List<string> items = new List<string>();
        Console.WriteLine("Start listing items. Press Enter after each item:");

        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        while (DateTime.Now < endTime)
        {
            Console.Write("Item: ");
            items.Add(Console.ReadLine());
        }

        Console.WriteLine($"\nYou listed {items.Count} items:");
        foreach (var item in items)
        {
            Console.WriteLine($"- {item}");
        }
        EndActivity();
    }
}

// Main Program
class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Mindfulness Program!");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Choose an option (1-4): ");

            string choice = Console.ReadLine();

            MindfulnessActivity activity = choice switch
            {
                "1" => new BreathingActivity(),
                "2" => new ReflectionActivity(),
                "3" => new ListingActivity(),
                "4" => null,
                _ => throw new InvalidOperationException("Invalid choice. Please choose a number between 1 and 4.")
            };

            if (activity == null)
            {
                Console.WriteLine("Thank you for using the Mindfulness Program. Goodbye!");
                break;
            }

            activity.PerformActivity();
        }
    }
}
