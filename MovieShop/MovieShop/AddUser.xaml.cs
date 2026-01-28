using System.Windows;
using System.Windows.Controls;

namespace MovieShop;

public partial class AddUser : Window
{
    public Uzytkownik NowyUzytkownik { get; private set; }
    
    public AddUser()
    {
        InitializeComponent();
    }
    
    private void AddUserCancel_OnClick(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }

    private void AddUserAdd_OnClick(object sender, RoutedEventArgs e)
    {
        string nazwa = TxtNazwa.Text;
        string rola = (RoleBox.SelectedItem as ComboBoxItem)?.Content.ToString();

        if (string.IsNullOrWhiteSpace(nazwa) || rola == null)
        {
            MessageBox.Show("Uzupełnij wszystkie pola");
            return;
        }

        if (rola == "Admin")
        {
            NowyUzytkownik = new Admin(nazwa);
        }
        else if (rola == "Klient")
        {
            NowyUzytkownik = new Klient(nazwa, 0);
        }

        DialogResult = true;
        Close();
    }
}