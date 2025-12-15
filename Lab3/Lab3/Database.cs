namespace Lab3;

public class Database
{
    public static List<Vehicle> Vehicle { get; set; } =
    [
        new Bike(0.6, "Yamaha", 2025),
        new Bike(0.9, "Kawasaki", 2023),
        new Car(2.0, "VW", 2006),
        new Car(1.0, "Fiat", 2018),
    ];
}