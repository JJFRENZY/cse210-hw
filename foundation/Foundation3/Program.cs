using System;

using System;
using System.Collections.Generic;

// Base class: Activity
public abstract class Activity
{
    // Shared attributes
    private DateTime date;
    private int durationInMinutes;

    // Constructor
    public Activity(DateTime date, int durationInMinutes)
    {
        this.date = date;
        this.durationInMinutes = durationInMinutes;
    }

    // Encapsulation: private getters for date and duration
    public DateTime Date => date;
    public int DurationInMinutes => durationInMinutes;

    // Abstract methods that must be overridden in derived classes
    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    // Summary method (using other methods to get values)
    public virtual string GetSummary()
    {
        string activityType = this.GetType().Name;
        return $"{Date:dd MMM yyyy} {activityType} ({DurationInMinutes} min): " +
               $"Distance {GetDistance():0.0} km, Speed: {GetSpeed():0.0} kph, Pace: {GetPace():0.0} min per km";
    }
}

// Derived class: Running
public class Running : Activity
{
    private double distance; // in kilometers

    public Running(DateTime date, int durationInMinutes, double distance) : base(date, durationInMinutes)
    {
        this.distance = distance;
    }

    // Override methods for running specifics
    public override double GetDistance()
    {
        return distance; // Return distance in kilometers
    }

    public override double GetSpeed()
    {
        return (GetDistance() / DurationInMinutes) * 60; // Speed = distance / time * 60
    }

    public override double GetPace()
    {
        return DurationInMinutes / GetDistance(); // Pace = time / distance
    }
}

// Derived class: Cycling
public class Cycling : Activity
{
    private double speed; // in kilometers per hour

    public Cycling(DateTime date, int durationInMinutes, double speed) : base(date, durationInMinutes)
    {
        this.speed = speed;
    }

    // Override methods for cycling specifics
    public override double GetDistance()
    {
        return (speed * DurationInMinutes) / 60; // Distance = speed * time / 60
    }

    public override double GetSpeed()
    {
        return speed; // Speed is provided directly
    }

    public override double GetPace()
    {
        return 60 / speed; // Pace = 60 / speed
    }
}

// Derived class: Swimming
public class Swimming : Activity
{
    private int laps; // Number of laps swum

    public Swimming(DateTime date, int durationInMinutes, int laps) : base(date, durationInMinutes)
    {
        this.laps = laps;
    }

    // Override methods for swimming specifics
    public override double GetDistance()
    {
        // Swimming distance: laps * 50 meters per lap / 1000 to convert to kilometers
        return (laps * 50) / 1000.0;
    }

    public override double GetSpeed()
    {
        return (GetDistance() / DurationInMinutes) * 60; // Speed = distance / time * 60
    }

    public override double GetPace()
    {
        return DurationInMinutes / GetDistance(); // Pace = time / distance
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Create sample activities
        var activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 3.0),
            new Cycling(new DateTime(2022, 11, 3), 30, 20.0),
            new Swimming(new DateTime(2022, 11, 3), 30, 20)
        };

        // Iterate through activities and display summaries
        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
