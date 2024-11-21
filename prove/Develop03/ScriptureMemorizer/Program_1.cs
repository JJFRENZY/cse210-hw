using System;

class Program_1
{
    static void Main(string[] args)
    {
        // Initialize a Scripture
        var reference = new ScriptureReference("Proverbs", "3:5", "3:6");
        var scripture = new Scripture(reference, "Trust in the Lord with all thine heart and lean not unto thine own understanding");

        while (true)
        {
            scripture.Display();

            if (scripture.IsFullyHidden())
            {
                Console.WriteLine("\nAll words are hidden! Well done.");
                break;
            }

            Console.WriteLine("\nPress Enter to continue or type 'quit' to exit.");
            var input = Console.ReadLine();
            if (input?.ToLower() == "quit")
            {
                break;
            }

            scripture.HideRandomWords();
        }
    }
}
