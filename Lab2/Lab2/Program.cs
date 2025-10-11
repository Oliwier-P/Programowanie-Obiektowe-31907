// // Zad 4
//
// Osoba osoba1 = new Osoba("Tom", 34);
// Osoba osoba2 = new Osoba("Jan", 14);
// Osoba osoba3 = new Osoba("Kuba", 7);
// Osoba osoba4 = new Osoba("Julka", 67);
// Osoba osoba5 = new Osoba("Oli", 18);
// osoba1.PrzedstawSie();
// osoba2.PrzedstawSie();
// osoba3.PrzedstawSie();
// osoba4.PrzedstawSie();
// osoba5.PrzedstawSie();
//
// class Osoba
// {
//     public string Imie;W
//     public int Wiek;
//
//     public Osoba(string imie, int wiek)
//     {
//         Imie = imie;
//         Wiek = wiek;
//     }
//     public void PrzedstawSie()
//     {
//         Console.WriteLine("Mam na imie "  + Imie + " i mam " + Wiek + " lat");
//     }
// }

// Zad 5
//
// KontoBankowe konto1 = new KontoBankowe();
// konto1.Wplata(200);
// konto1.Wyplata(100);
// KontoBankowe konto2 = new KontoBankowe();
// konto2.Wplata(500);
// konto2.Wyplata(1000);
//
// class KontoBankowe
// {
//     private double saldo;
//     
//     public void Wplata(double kwota) { saldo += kwota; }
//
//     public void Wyplata(double kwota)
//     {
//         if (saldo >= kwota)
//         {
//             saldo -= kwota;
//             Console.WriteLine("Nowe saldo: " + saldo);
//         }
//         else
//         {
//             Console.WriteLine("Za mało środków na koncie");
//         }
//     }
// }

// Zad 6
//
// Pies pies = new Pies();
// pies.Jedz();
// pies.Szczekaj();
// Kot kot = new Kot();
// kot.Jedz();
// kot.Miaucz();
//
// class Zwierze
// {
//     public void Jedz() => Console.WriteLine("Zwierzę je");
// }
// class Pies : Zwierze
// {
//     public void Szczekaj() => Console.WriteLine("Hau hau!");
// }
// class Kot : Zwierze
// {
//     public void Miaucz() => Console.WriteLine("Miauuu!");
// }