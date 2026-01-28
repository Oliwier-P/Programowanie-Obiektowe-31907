using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Text.Json;
using System.IO;
using Path = System.IO.Path;

namespace MovieShop;
public partial class MainWindow : Window
{
    private Uzytkownik AktualnyUzytkownik;
    private string AktualnyWidok;
    
    public MainWindow()
    {
        InitializeComponent();
        
        BtnDodaj.Visibility = Visibility.Collapsed;
        BtnUsun.Visibility = Visibility.Collapsed;
        BtnOddaj.Visibility = Visibility.Collapsed;
        BtnWypozycz.Visibility =  Visibility.Collapsed;
        BtnKup.Visibility = Visibility.Collapsed;
        BtnWybierz.Visibility = Visibility.Collapsed;
        
        var uzytkownicy = WczytajUzytkownikowZPliku();
        AktualnyUzytkownik = uzytkownicy.FirstOrDefault(u => u.Rola == "Admin");
    }

    private void ZapiszTwojeFilmyDoPliku(Dictionary<string, List<TwojFilm>> lista)
    {
        var json = JsonSerializer.Serialize(lista, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText("twojefilmy.json", json);
    }
    
    private void ZapiszUzytkownikowDoPliku(List<Uzytkownik> lista)
    {
        var daneDoZapisu = lista.Select<Uzytkownik, object>(u =>
        {
            if (u is Admin)
            {
                return new
                {
                    Rola = "Admin",
                    Nazwa = u.Nazwa
                };
            }
            else if (u is Klient k)
            {
                return new
                {
                    Rola = "Klient",
                    Nazwa = k.Nazwa,
                    IloscFilmow = k.IloscFilmow
                };
            }

            return null;
        });

        var json = JsonSerializer.Serialize(daneDoZapisu, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText("uzytkownicy.json", json);
    }
    
    private void ZapiszFilmyDoPliku(List<Film> lista)
    {
        var json = JsonSerializer.Serialize(lista, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText("filmy.json", json);
    }

    
    private List<Uzytkownik> WczytajUzytkownikowZPliku()
    {
        var lista = new List<Uzytkownik>();
        
        string json = File.ReadAllText("uzytkownicy.json");
        var dokument = JsonDocument.Parse(json);
    
        foreach (var element in dokument.RootElement.EnumerateArray())
        {
            string rola = element.GetProperty("Rola").GetString();
            string nazwa = element.GetProperty("Nazwa").GetString();

            switch (rola)
            {
                case "Admin":
                    lista.Add(new Admin(nazwa));
                    break;

                case "Klient":
                    int iloscFilmow = element.GetProperty("IloscFilmow").GetInt32();
                    lista.Add(new Klient(nazwa, iloscFilmow));
                    break;
                default:
                    break;
            }
        }
    
        return lista;
    }

    private List<Film> WczytajFilmyZPliku()
    {
        var filmyJson = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "filmy.json"));
        var filmy = JsonSerializer.Deserialize<List<Film>>(filmyJson);
        
        return filmy;
    }

    private Dictionary<string, List<TwojFilm>> WczytajWszystkieTwojeFilmyZPliku()
    {
        if (!File.Exists("twojefilmy.json"))
            return new Dictionary<string, List<TwojFilm>>();

        var json = File.ReadAllText("twojefilmy.json");

        return JsonSerializer.Deserialize<Dictionary<string, List<TwojFilm>>>(json)
               ?? new Dictionary<string, List<TwojFilm>>();
    }
    
    private void UstawWidokDlaRoli()
    {
        BtnDodaj.Visibility = Visibility.Collapsed;
        BtnUsun.Visibility = Visibility.Collapsed;
        BtnOddaj.Visibility = Visibility.Collapsed;
        BtnWypozycz.Visibility =  Visibility.Collapsed;
        BtnKup.Visibility = Visibility.Collapsed;
        BtnWybierz.Visibility = Visibility.Collapsed;
        
        if (AktualnyWidok == "Klienci")
        {
            if (AktualnyUzytkownik.Nazwa == "Admin")
            {
                BtnDodaj.Visibility = Visibility.Visible;
                BtnUsun.Visibility = Visibility.Visible;
            }
            
            BtnWybierz.Visibility = Visibility.Visible;
        }
        else if (AktualnyWidok == "Filmy")
        {
            if (AktualnyUzytkownik.Nazwa == "Admin")
            {
                BtnDodaj.Visibility = Visibility.Visible;
                BtnUsun.Visibility = Visibility.Visible;
            }
            else
            {
                BtnWypozycz.Visibility = Visibility.Visible;
                BtnKup.Visibility = Visibility.Visible;
            }
        }
        else if (AktualnyWidok == "TwojeFilmy")
        {
            if (AktualnyUzytkownik.Nazwa != "Admin")
            {
                BtnOddaj.Visibility = Visibility.Visible;
            }
        }
    }
    
    private GridViewColumn StworzKolumne(string naglowek, string binding, int szerokosc)
    {
        return new GridViewColumn
        {
            Header = naglowek,
            Width = szerokosc,
            DisplayMemberBinding = new Binding(binding)
        };
    }

    private void UstawKolumneFilmy()
    {
        var gridView = new GridView();

        gridView.Columns.Add(StworzKolumne("Nazwa", "Nazwa", 250));
        gridView.Columns.Add(StworzKolumne("Gatunek", "Gatunek", 150));
        gridView.Columns.Add(StworzKolumne("Rok", "Rok", 100));
        gridView.Columns.Add(StworzKolumne("Ilość", "Ilosc", 100));
        gridView.Columns.Add(StworzKolumne("Cena", "Cena", 130));

        MainList.View = gridView;
    }
    
    private void UstawKolumnyDlaKlientow()
    {
        var gridView = new GridView();

        gridView.Columns.Add(StworzKolumne("Nazwa", "Nazwa", 200));
        gridView.Columns.Add(StworzKolumne("Ilość filmów", "IloscFilmow", 200));

        MainList.View = gridView;
    }
    
    private void UstawKolumneTwojeFilmy()
    {
        var gridView = new GridView();

        gridView.Columns.Add(StworzKolumne("Nazwa", "Nazwa", 150));
        gridView.Columns.Add(StworzKolumne("Data zakupu", "DataZakupu", 200));
        gridView.Columns.Add(StworzKolumne("Data oddania", "DataOddania", 200));
        gridView.Columns.Add(StworzKolumne("Typ", "Typ", 200));

        MainList.View = gridView;
    }
    
    private void Filmy_Click(object sender, RoutedEventArgs e)
    {
        AktualnyWidok = "Filmy";
        UstawKolumneFilmy();
        UstawWidokDlaRoli();

        MainList.ItemsSource = WczytajFilmyZPliku();
    }

    private void Klienci_Click(object sender, RoutedEventArgs e)
    {
        AktualnyWidok = "Klienci";
        UstawKolumnyDlaKlientow();
        UstawWidokDlaRoli();
        
        MainList.ItemsSource = WczytajUzytkownikowZPliku();
    }

    private void Twoje_Filmy_Click(object sender, RoutedEventArgs e)
    {
        AktualnyWidok = "TwojeFilmy";
        UstawKolumneTwojeFilmy();
        UstawWidokDlaRoli();

        if (AktualnyUzytkownik.Rola == "Admin")
        {
            MainList.ItemsSource = new List<object>();;
            return;
        }
        
        var wszystkieFilmy = WczytajWszystkieTwojeFilmyZPliku();
        if (wszystkieFilmy.TryGetValue(AktualnyUzytkownik.Nazwa, out var listaFilmow))
        {
            MainList.ItemsSource = listaFilmow;
        }
        else
        {
            MainList.ItemsSource = new List<TwojFilm>();
        }
    }

    private void BtnWybierz_OnClick(object sender, RoutedEventArgs e)
    {
        if (MainList.SelectedItem is Uzytkownik uzytkownik)
        {
            AktualnyUzytkownik = uzytkownik;
            Uzytkownik.Text = AktualnyUzytkownik.Nazwa;
            UstawWidokDlaRoli();
        }
    }

    private void SearchBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        string SearchText = SearchBox.Text.ToLower();
        
        switch (AktualnyWidok)
        {
            case  "Filmy":
                var filmy = WczytajFilmyZPliku();
                MainList.ItemsSource =  filmy.Where(f => f.Nazwa.ToLower().Contains(SearchText)).ToList();
                break;
            case  "Klienci":
                var uzytkownicy = WczytajUzytkownikowZPliku();
                MainList.ItemsSource =  uzytkownicy.Where(u => u.Nazwa.ToLower().Contains(SearchText)).ToList();
                break;
            case  "TwojeFilmy":
                var wszystkieFilmy = WczytajWszystkieTwojeFilmyZPliku();
                if (wszystkieFilmy.TryGetValue(AktualnyUzytkownik.Nazwa, out var listaFilmow))
                {
                    MainList.ItemsSource = listaFilmow;
                }
                else
                {
                    MainList.ItemsSource = new List<TwojFilm>();
                }
                break;
            default:
                break;
        }
    }
    
     private void UsunTwojFilm(TwojFilm wybranyElement)
     {
         var lista  = WczytajWszystkieTwojeFilmyZPliku();

         if (lista.TryGetValue(AktualnyUzytkownik.Nazwa, out var listaFilmow))
         {
             listaFilmow.RemoveAll(f =>
                 f.Nazwa == wybranyElement.Nazwa &&
                 f.DataZakupu == wybranyElement.DataZakupu);

             ZapiszTwojeFilmyDoPliku(lista);
         }
     }
     
    private void BtnUsun_OnClick(object sender, RoutedEventArgs e)
    {
        var wybranyElement = MainList.SelectedItem;
        
        if (wybranyElement == null)
        {
            MessageBox.Show("Nie wybrano żadnego elementu!");
            return;
        }
        
        switch (AktualnyWidok)
        {
            case "Filmy":
                UsunFilm((Film)wybranyElement);
                MessageBox.Show("Pomyslnie usunięto film");
                break;
            case "Klienci":
                UsunUzytkownika((Uzytkownik)wybranyElement);
                MessageBox.Show("Pomyslnie usunięto uzytkownika");
                break;
            default:
                break;
        }

        void UsunFilm(Film wybranyElement)
        {
            var lista = WczytajFilmyZPliku();
            
            lista.RemoveAll(f => f.Nazwa == wybranyElement.Nazwa);
            
            ZapiszFilmyDoPliku(lista);

            MainList.ItemsSource = lista;
        }

        void UsunUzytkownika(Uzytkownik wybranyElement)
        {
            var lista  = WczytajUzytkownikowZPliku();
            
            lista.RemoveAll(u => u.Nazwa == wybranyElement.Nazwa);

            ZapiszUzytkownikowDoPliku(lista);
            
            MainList.ItemsSource = lista;
        }
    }

    private void BtnDodaj_OnClick(object sender, RoutedEventArgs e)
    {
        switch (AktualnyWidok)
        {
            case "Filmy":
                DodajFilm();
                break;
            case "Klienci":
                DodajUzytkownika();
                break;
            default:
                break;
        }

        void DodajFilm()
        {
            var okno = new AddFilmWindow();
            okno.Owner = this;
            
            if (okno.ShowDialog() == true)
            {
                var film = okno.NowyFilm;

                var lista = WczytajFilmyZPliku();
                lista.Add(film);

                ZapiszFilmyDoPliku(lista);

                MainList.ItemsSource = lista;

                MessageBox.Show("Pomyslnie dodano film");
            }
        }
        
        void DodajUzytkownika()
        {
            var okno = new AddUser();
            okno.Owner = this;
            
            if (okno.ShowDialog() == true)
            {
                var uzytkownik = okno.NowyUzytkownik;

                var lista = WczytajUzytkownikowZPliku();
                lista.Add(uzytkownik);

                ZapiszUzytkownikowDoPliku(lista);

                var twojefilmy = WczytajWszystkieTwojeFilmyZPliku(); 
                
                if (!twojefilmy.ContainsKey(uzytkownik.Nazwa))
                {
                    twojefilmy[uzytkownik.Nazwa] = new List<TwojFilm>();
                }
                
                var json = JsonSerializer.Serialize(twojefilmy, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText("twojefilmy.json", json);
                
                MainList.ItemsSource = lista;
                
                MessageBox.Show("Pomyslnie dodano uzytkownika");
            }
        }
    }
    
    private void ZmniejszIloscFilmu(Film wybranyFilm)
    {
        var filmy = WczytajFilmyZPliku();
            
        var film = filmy.FirstOrDefault(f => f.Nazwa == wybranyFilm.Nazwa);
            
        if (film.Ilosc <= 0)
        {
            MessageBox.Show("Brak filmu na stanie!");
            return;
        }
            
        film.Ilosc--;
            
        ZapiszFilmyDoPliku(filmy);
    }
    
    private void ZwiekszIloscFilmu(string nazwaFilmu)
    {
        var filmy = WczytajFilmyZPliku();
            
        var film = filmy.FirstOrDefault(f => f.Nazwa == nazwaFilmu);
            
        film.Ilosc++;
            
        ZapiszFilmyDoPliku(filmy);
    }
    
    private void DodajFilmDoUzytkownika(Film wybranyFilm, bool czyKupiony)
    {
        var filmy = WczytajWszystkieTwojeFilmyZPliku();
        
        if (!filmy.ContainsKey(AktualnyUzytkownik.Nazwa))
        {
            filmy[AktualnyUzytkownik.Nazwa] = new List<TwojFilm>();
        }
    
        var nowyFilm = new TwojFilm
        {
            Nazwa = wybranyFilm.Nazwa,
            DataZakupu = DateTime.Now.ToString("dd-MM-yyyyy"),
            DataOddania = czyKupiony ? null : DateTime.Now.AddDays(10).ToString("dd-MM-yyyy"),
            Typ = czyKupiony ? "Kupiony" : "Wypozyczony"
        };
    
        filmy[AktualnyUzytkownik.Nazwa].Add(nowyFilm);

        ZapiszTwojeFilmyDoPliku(filmy);
        
        var uzytkownicy = WczytajUzytkownikowZPliku();
        
        var klient = uzytkownicy
            .OfType<Klient>()
            .FirstOrDefault(u => u.Nazwa == AktualnyUzytkownik.Nazwa);

        if (klient != null)
        {
            klient.IloscFilmow = filmy[AktualnyUzytkownik.Nazwa].Count;

            ZapiszUzytkownikowDoPliku(uzytkownicy);
        }
        
        if (!czyKupiony)
        {
            MessageBox.Show("Pomyślnie wypozyczono film: " + nowyFilm.Nazwa);
        }
        else
        {
            MessageBox.Show("Pomyślnie kupiono film: " + nowyFilm.Nazwa );
        }
    }
    
    private void BtnWypozycz_OnClick(object sender, RoutedEventArgs e)
    {
        var wybranyElement = MainList.SelectedItem;
        
        if (wybranyElement == null)
        {
            MessageBox.Show("Nie wybrano żadnego elementu!");
            return;
        }

        if (wybranyElement is Film)
        {
            ZmniejszIloscFilmu((Film)wybranyElement);
            DodajFilmDoUzytkownika((Film)wybranyElement, false);
        }
    }

    private void BtnKup_OnClick(object sender, RoutedEventArgs e)
    {
        var wybranyElement = MainList.SelectedItem;
        
        if (wybranyElement == null)
        {
            MessageBox.Show("Nie wybrano żadnego elementu!");
            return;
        }

        if (wybranyElement is Film)
        {
            ZmniejszIloscFilmu((Film)wybranyElement);
            DodajFilmDoUzytkownika((Film)wybranyElement, true);
        }
    }
    
    private void BtnEdytuj_OnClick(object sender, RoutedEventArgs e)
    {
    }

    private void BtnOddaj_OnClick(object sender, RoutedEventArgs e)
    {
        var wybranyElement = MainList.SelectedItem;
        
        if (wybranyElement == null)
        {
            MessageBox.Show("Nie wybrano żadnego elementu!");
            return;
        }
        
        var nazwaFilmu = wybranyElement as TwojFilm;
        
        UsunTwojFilm((TwojFilm)wybranyElement);
        ZwiekszIloscFilmu(nazwaFilmu.Nazwa);
        
        var uzytkownicy = WczytajUzytkownikowZPliku();
            
        var uzytkownik = uzytkownicy.OfType<Klient>().FirstOrDefault(u => u.Nazwa == AktualnyUzytkownik.Nazwa);

        uzytkownik.IloscFilmow--;
        
        ZapiszUzytkownikowDoPliku(uzytkownicy);

        var wszystkieFilmy = WczytajWszystkieTwojeFilmyZPliku();
        if (wszystkieFilmy.TryGetValue(AktualnyUzytkownik.Nazwa, out var listaFilmow))
        {
            MainList.ItemsSource = listaFilmow;
        }
        else
        {
            MainList.ItemsSource = new List<TwojFilm>();
        }

        MessageBox.Show("Pomyślnie oddano film");
    }
}