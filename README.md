# C--Assignment1

**Assignment - Administracija korisnika**

Zadatak
Kreirati WPF aplikaciju koja koristi MVVM šablon i koja ima funkcionalnosti opisane kroz sledeće radne zadatke:

Kreirati bazu podataka ADMIN_1 i tabelu Users sa kolonama ID, UserName, UserPass, IsAdmin.
Napraviti LoginWindow prozor u kom će korisnik da unese korisničko ime i šifru. Pritiskom na dugme Login proveravate da li korisnik sa navedenim podacima postoji u bazi sa navedenim podacima; ako postoji, prikazati prozor MainWindow. Ukoliko ne postoji korisnik sa navedenim podacima, prikazati mu poruku „Login failed. Please try again.”
Napraviti prozor MainWindow koji će da sadrži ListBox kontrolu sa prikazom korisničkih imena svih korisnika iz tabele Users u bazi.
Omogućiti pregled i izmenu podataka za selektovanog korisnika iz liste. Omogućite korisniku da, kada selektuje red u ListBox kontroli, vidi sve podatke o tom korisniku (korisničko ime, šifru, da li je u pitanju administrator (boolean podatak)). Korisnik može da izmeni neki od podataka i klikom na dugme Save izmene će se snimiti u bazu.
Omogućiti dodavanje novog korisnika u bazu.
 

Aplikaciju isporučiti u vidu Visual Studio projekta, a bazu podataka u vidu SQL skriptova (u tekstualnom dokumentu). U SSMS-u odradite Tasks-> Generate Scripts, ispratite korake i generisaće se .sql fajl koji je script baze.

Napomena: Bazu podataka ne treba isporučivati u binarnoj formi (bekap ili .mdf fajl). Da biste smanjili veličinu arhive u kojoj se nalazi zapakovan folder sa VS projektom, iz arhive obrišite packages, bin i obj foldere.
