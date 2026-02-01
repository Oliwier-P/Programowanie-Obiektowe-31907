using System.Windows;

namespace MovieShop;

public partial class AddFilmWindow : Window
{
    public Film NowyFilm { get; private set; }
    
    public AddFilmWindow()
    {
        InitializeComponent();
    }

    private void AddFilmCancel_OnClick(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }

    private void AddFilmAdd_OnClick(object sender, RoutedEventArgs e)
    {
        string nazwa = TxtNazwa.Text;
        string gatunek = TxtGatunek.Text;
        int rok = int.Parse(TxtRok.Text);
        int ilosc = int.Parse(TxtIlosc.Text);
        decimal cena = decimal.Parse(TxtCena.Text);
        
        if (string.IsNullOrWhiteSpace(nazwa) || string.IsNullOrWhiteSpace(gatunek) || rok <= 0 || ilosc <= 0 || cena <= 0)
        {
            MessageBox.Show("Uzupełnij wszystkie pola");
            return;
        }
        
        NowyFilm = new Film(nazwa, gatunek, rok, ilosc, cena);
        
        DialogResult = true;
        Close();
    }
}