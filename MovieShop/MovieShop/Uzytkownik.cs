using System.Text.Json.Serialization;

namespace MovieShop;

public abstract class Uzytkownik
{   
    [JsonInclude]
    public string Nazwa { get; protected set; }
    public abstract string Rola { get; }

    protected Uzytkownik(string nazwa)
    {
        Nazwa = nazwa;
    }
}