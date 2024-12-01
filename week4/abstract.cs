using System;
using System.Collections.Generic;

namespace YouTubeTracker
{
    // Comment Class
    public class Comment
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public Comment(string name, string text)
        {
            Name = name;
            Text = text;
        }

        public override string ToString()
        {
            return $"{Name}: {Text}";
        }
    }

    // Video Class
    public class Video
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Length { get; set; } // Length in seconds
        private List<Comment> Comments { get; set; }

        public Video(string title, string author, int length)
        {
            Title = title;
            Author = author;
            Length = length;
            Comments = new List<Comment>();
        }

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        public int GetCommentCount()
        {
            return Comments.Count;
        }

        public void DisplayDetails()
        {
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Length: {Length / 60}m {Length % 60}s");
            Console.WriteLine($"Comments ({GetCommentCount()}):");
            foreach (var comment in Comments)
            {
                Console.WriteLine($"  {comment}");
            }
            Console.WriteLine(new string('-', 40));
        }
    }

    // Main Program
    class Program
    {
        static void Main(string[] args)
        {
            // Create videos
            var video1 = new Video("How to Cook Pasta", "Chef John", 300);
            var video2 = new Video("Understanding Quantum Physics", "Dr. Smith", 720);
            var video3 = new Video("Top 10 Travel Destinations", "Explorer Jane", 540);

            // Add comments to videos
            video1.AddComment(new Comment("Alice", "Great recipe!"));
            video1.AddComment(new Comment("Bob", "Tried it and loved it."));
            video1.AddComment(new Comment("Charlie", "Perfect for dinner tonight."));

            video2.AddComment(new Comment("Dave", "Mind-blowing explanation!"));
            video2.AddComment(new Comment("Eve", "Quantum mechanics is fascinating."));
            video2.AddComment(new Comment("Frank", "Can you make more videos on this topic?"));

            video3.AddComment(new Comment("Grace", "Added all these places to my bucket list!"));
            video3.AddComment(new Comment("Heidi", "Amazing destinations."));
            video3.AddComment(new Comment("Ivan", "I want to visit all of them!"));

            // Store videos in a list
            var videos = new List<Video> { video1, video2, video3 };

            // Display video details and comments
            foreach (var video in videos)
            {
                video.DisplayDetails();
            }
        }
    }
}
