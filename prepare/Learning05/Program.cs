using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    // Base class for all activities
    abstract class MindfulnessActivity
    {
        protected string Name { get; set; }
        protected string Description { get; set; }
        protected int Duration { get; set; }

        public void StartActivity()
        {
            Console.Clear();
            Console.WriteLine($"Starting {Name}...");
            Console.WriteLine($"{Description}");
            Console.Write("Enter duration in seconds: ");
            Duration = int.Parse(Console.ReadLine());
            Console.WriteLine("Prepare to begin...");
            DisplayAnimation();
        }

        public void FinishActivity()
        {
            Console.WriteLine("Good job!");
            Console.WriteLine($"You completed {Name} for {Duration} seconds.");
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

        public abstract void PerformActivity();
    }

    // Breathing activity
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
            for (int i = 0; i < Duration / 4; i++) // Adjust for inhale/exhale
            {
                Console.WriteLine("Breathe in...");
                Thread.Sleep(2000); // 2 seconds
                Console.WriteLine("Breathe out...");
                Thread.Sleep(2000); // 2 seconds
            }
            FinishActivity();
        }
    }

    // Reflection activity
    class ReflectionActivity : MindfulnessActivity
    {
        private List<string> Prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private List<string> Questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "What did you learn about yourself?",
            "How can you apply this experience in the future?"
        };

        public ReflectionActivity()
        {
            Name = "Reflection Activity";
            Description = "Reflect on times in your life when you have shown strength and resilience.";
        }

        public override void PerformActivity()
        {
            StartActivity();
            Random rand = new Random();
            Console.WriteLine(Prompts[rand.Next(Prompts.Count)]);
            for (int i = 0; i < Duration / 5; i++) // Adjust for questions
            {
                Console.WriteLine(Questions[rand.Next(Questions.Count)]);
                DisplayAnimation();
            }
            FinishActivity();
        }
    }

    // Listing activity
    class ListingActivity : MindfulnessActivity
    {
        private List<string> Prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are some of your personal heroes?"
        };

        public ListingActivity()
        {
            Name = "Listing Activity";
            Description = "Reflect on positive aspects by listing items related to the prompt.";
        }

        public override void PerformActivity()
        {
            StartActivity();
            Random rand = new Random();
            Console.WriteLine(Prompts[rand.Next(Prompts.Count)]);
            Console.WriteLine("Start listing items:");
            int count = 0;
            DateTime endTime = DateTime.Now.AddSeconds(Duration);
            while (DateTime.Now < endTime)
            {
                Console.Write("> ");
                Console.ReadLine();
                count++;
            }
            Console.WriteLine($"You listed {count} items.");
            FinishActivity();
        }
    }

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            MindfulnessActivity activity;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    Thread.Sleep(2000);
                    continue;
            }

            activity.PerformActivity();
        }
    }
}
