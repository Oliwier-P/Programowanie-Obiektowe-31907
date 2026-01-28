namespace MovieShop;

public class Klient : Uzytkownik
{
    public int IloscFilmow { get; set; }

    public override string Rola => "User";

    public Klient(string nazwa, int iloscFilmow)
        : base(nazwa)
    {
        IloscFilmow = iloscFilmow;
    }
}