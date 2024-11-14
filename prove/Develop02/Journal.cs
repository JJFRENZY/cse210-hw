// Journal.cs
using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayEntries()
    {
        foreach (var entry in _entries)
        {
            Console.WriteLine(entry);
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in _entries)
            {
                writer.WriteLine(entry.ToString());
            }
        }
        Console.WriteLine("Journal saved to " + filename);
    }

    public void LoadFromFile(string filename)
    {
        _entries.Clear();
        using (StreamReader reader = new StreamReader(filename))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split('|');
                if (parts.Length == 3)
                {
                    string date = parts[0].Trim();
                    string prompt = parts[1].Trim();
                    string response = parts[2].Trim();
                    _entries.Add(new Entry(date, prompt, response));
                }
            }
        }
        Console.WriteLine("Journal loaded from " + filename);
    }
}
