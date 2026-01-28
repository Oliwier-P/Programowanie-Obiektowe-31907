namespace MovieShop;

public class Admin : Uzytkownik
{
    public override string Rola => "Admin";

    public Admin(string nazwa) : base(nazwa) { }
}