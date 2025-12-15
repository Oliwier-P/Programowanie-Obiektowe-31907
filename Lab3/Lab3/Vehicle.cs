namespace Lab3;

public abstract class Vehicle
{
    public double EngineCapacity { get; protected set; }
    private string model;
    private int year;
    
    public string Model => model;
    public int Year => year;
    
    public Vehicle(double engineCapacity, string model, int year)
    {
        EngineCapacity = engineCapacity;
        this.model = model;
        this.year = year;
    }

    public virtual void Start()
    {
        Console.WriteLine("Vehicle started.");
    }
    public virtual void Stop()
    {
        Console.WriteLine("Vehicle stopped.");
    }
}