namespace MovieShop;

public class Film
{
    public string Nazwa { get; set; }
    public string Gatunek { get; set; }
    public int Rok { get; set; }
    public int Ilosc { get; set; }
    public decimal Cena { get; set; }

    public Film(string nazwa, string gatunek, int rok, int ilosc, decimal cena)
    {
        Nazwa = nazwa;
        Gatunek = gatunek;
        Rok = rok;
        Ilosc = ilosc;
        Cena = cena;
    }
}