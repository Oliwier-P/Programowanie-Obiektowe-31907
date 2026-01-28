namespace MovieShop;

public abstract class Uzytkownik
{
    public string Nazwa { get; protected set; }
    public abstract string Rola { get; }

    protected Uzytkownik(string nazwa)
    {
        Nazwa = nazwa;
    }
}