// Program.cs
using System;
using System.Collections.Generic;

public class Program
{
    private static Journal journal = new Journal();
    private static readonly List<string> prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    public static void Main(string[] args)
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    WriteNewEntry();
                    break;
                case "2":
                    journal.DisplayEntries();
                    break;
                case "3":
                    SaveJournal();
                    break;
                case "4":
                    LoadJournal();
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    private static void WriteNewEntry()
    {
        var random = new Random();
        string prompt = prompts[random.Next(prompts.Count)];
        Console.WriteLine(prompt);
        Console.Write("Your response: ");
        string response = Console.ReadLine();
        var entry = new Entry(DateTime.Now.ToString("yyyy-MM-dd"), prompt, response);
        journal.AddEntry(entry);
    }

    private static void SaveJournal()
    {
        Console.Write("Enter filename to save: ");
        string filename = Console.ReadLine();
        journal.SaveToFile(filename);
    }

    private static void LoadJournal()
    {
        Console.Write("Enter filename to load: ");
        string filename = Console.ReadLine();
        journal.LoadFromFile(filename);
    }
}
