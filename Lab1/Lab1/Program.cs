// Example 1

const int requiredAgeFullAccess = 18;
const int requiredAgeOpenAccess = 14;
const string accessDenied = "Musisz mieć 18 lat.";
const string accessAll = "Witamy w naszym sklepie";
const string accessOpen = "Masz dostęp do skelpu, ale nie mozesz kupić i zarejestrować karty SIM";

int age;

Console.WriteLine("Podaj swój wiek: ");

string input = Console.ReadLine();

bool success = int.TryParse(input, out age);

if (!success)
{
    Console.WriteLine("Podaj poprawną wartość!");
}
else
{
    if (age > requiredAgeFullAccess)
    {
        Console.WriteLine(accessAll);
    } else if (age > requiredAgeOpenAccess)
    {
        Console.WriteLine(accessOpen);
    } else
    {
        Console.WriteLine(accessDenied);
    }
}


// Example 2

// var names = new[] { "Artur", "Alicja", "Michał", "Gosia" };
//
// for (int i = 0; i < names.Length; i++)
// {
//     Console.WriteLine(names[i]);
// }
//
// foreach (var name in names)
// {
//     Console.WriteLine(name);
// }