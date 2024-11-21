using System;
using System.Collections.Generic;

public class Scripture
{
    public ScriptureReference Reference { get; private set; }
    private List<Word> Words;

    public Scripture(ScriptureReference reference, string text)
    {
        Reference = reference;
        Words = new List<Word>();
        foreach (var word in text.Split(' '))
        {
            Words.Add(new Word(word));
        }
    }

    public void Display()
    {
        Console.Clear();
        Console.WriteLine($"{Reference}\n");
        foreach (var word in Words)
        {
            Console.Write(word + " ");
        }
        Console.WriteLine();
    }

    public void HideRandomWords()
    {
        Random random = new Random();
        int countToHide = 3; // Number of words to hide per step.
        int hidden = 0;

        while (hidden < countToHide)
        {
            var index = random.Next(Words.Count);
            if (!Words[index].IsHidden)
            {
                Words[index].Hide();
                hidden++;
            }
        }
    }

    public bool IsFullyHidden()
    {
        return Words.TrueForAll(w => w.IsHidden);
    }
}
