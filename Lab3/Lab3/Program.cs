using System.Text.Json;
using Lab3;

var carsJson = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "cars.json"));
var bikesJson = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "bikes.json"));

var cars = JsonSerializer.Deserialize<List<Car>>(carsJson);
var bikes = JsonSerializer.Deserialize<List<Bike>>(bikesJson);

List<Vehicle> Vehicles()
{
    var list = new  List<Vehicle>();
    list.AddRange(cars);
    list.AddRange(bikes);
    return list;
}

bool run = true;
do
{
    Console.WriteLine("CAR SHOP");
    Console.WriteLine("[1] Show all, [2] Search by year, " + 
                      "[3] Search by model, [4] Search by engine capacity, " +
                      "[5] Add new vehicle, [0] Exit");
    var input = Console.ReadKey().KeyChar;

    Console.WriteLine("");
    
    switch (input)
    {
        case '1':
            DisplayVehicleModel();
            break;
        case '2':
            SearchByYear();
            break;
        case '3':
            SearchByModel();
            break;
        case '4':
            SearchByEngineCapacity();
            break;
        case '5':
            AddNewVehicle();
            break;
        case '0':
            run = false;
            break;
        default:
            Console.WriteLine("Invalid menu option!");
            break;
    }
} while (run);

Console.WriteLine("Goodbye...");
return;

void DisplayVehicleModel()
{
    Console.WriteLine("Our vehicles:");
    foreach (var vehicle in Vehicles())
    {
        Console.WriteLine(vehicle.Model);
    }
}

void SearchByYear()
{
    Console.Write("Enter year: ");
    var success = Int32.TryParse(Console.ReadLine(), out int year);
    if (!success)
    {
        Console.WriteLine("Invalid year!");
        SearchByYear();
        return;
    }
    
    var vehicles = Vehicles().Where(x => x.Year == year);

    if (!vehicles.Any())
    {
        Console.WriteLine("No vehicles found!");
        return;
    }
    else
    {
        foreach (var vehicle in vehicles)
        {
            Console.WriteLine(vehicle.Model);
        }
    }
}

void SearchByModel()
{
    Console.Write("Enter model: ");
    string? model = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(model))
    {
        Console.WriteLine("Invalid model!");
        SearchByModel();
        return;
    }
    
    var vehicles = Vehicles().Where(x => x.Model.ToLower() == model.ToLower() );

    if (!vehicles.Any())
    {
        Console.WriteLine("No vehicles found!");
        return;
    }
    else
    {
        foreach (var vehicle in vehicles)
        {
            Console.WriteLine(vehicle.Model);
        }
    }
}

void SearchByEngineCapacity()
{
    Console.WriteLine("Enter engine capacity: ");
    var success = double.TryParse(Console.ReadLine(), out double engineCapacity);
    
    if (!success)
    {
        Console.WriteLine("Invalid engine capacity!");
        return;
    }
    
    var vehicles = Vehicles().Where(x => x.EngineCapacity == engineCapacity );

    if (!vehicles.Any())
    {
        Console.WriteLine("No vehicles found!");
        return;
    }
    else
    {
        foreach (var vehicle in vehicles)
        {
            Console.WriteLine(vehicle.Model);
        }
    }
}

void AddNewVehicle()
{
    Console.WriteLine("B for bike, C for car: ");
    var input = Console.ReadKey().KeyChar;
    Console.WriteLine("");
    
    if (input.ToString().ToLower() is not ("b" or "c"))
    {
        Console.WriteLine("Invalid vehicle type!");
        return;
    }
    
    Console.WriteLine("Enter engine capacity: ");
    var success = double.TryParse(Console.ReadLine(), out double engineCapacity);
    
    if (!success)
    {
        Console.WriteLine("Invalid engine capacity!");
        return;
    }

    Console.WriteLine("Enter model: ");
    string? model = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(model))
    {
        Console.WriteLine("Invalid model!");
        return;
    }
    
    Console.WriteLine("Enter year: ");
    success = Int32.TryParse(Console.ReadLine(), out int year);
    
    if (!success)
    {
        Console.WriteLine("Invalid year!");
        return;
    }

    Vehicle v;
    
    if (input.ToString().ToLower() == "c")
    {
        v = new Car(engineCapacity, model, year);
    }
    else
    {
        v = new Bike(engineCapacity, model, year);
    }
    
    Vehicles().Add(v);
}