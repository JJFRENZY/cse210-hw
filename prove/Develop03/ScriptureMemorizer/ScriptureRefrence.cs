public class ScriptureReference
{
    public string Book { get; private set; }
    public string StartVerse { get; private set; }
    public string EndVerse { get; private set; }

    public ScriptureReference(string book, string startVerse)
    {
        Book = book;
        StartVerse = startVerse;
        EndVerse = null;
    }

    public ScriptureReference(string book, string startVerse, string endVerse)
    {
        Book = book;
        StartVerse = startVerse;
        EndVerse = endVerse;
    }

    public override string ToString()
    {
        return EndVerse == null ? $"{Book} {StartVerse}" : $"{Book} {StartVerse}-{EndVerse}";
    }
}
