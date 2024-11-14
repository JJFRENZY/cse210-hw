// Entry.cs
using System;

public class Entry
{
    public string Date { get; private set; }
    public string Prompt { get; private set; }
    public string Response { get; private set; }

    public Entry(string date, string prompt, string response)
    {
        Date = date;
        Prompt = prompt;
        Response = response;
    }

    public override string ToString()
    {
        return $"{Date} | {Prompt} | {Response}";
    }
}
