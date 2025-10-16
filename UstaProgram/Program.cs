using System;
using System.Text;
using System.Collections.Generic;
using UstaPlatform.Alan;
using UstaPlatform.Altyapi;
using UstaPlatform.Fiyatlandirma;

var ustalar = new List<Usta>
{
    new Usta { Id = 1, Ad = "Mehmet Usta", Uzmanlik = "Tesisatçı", Puan = 4.7, GunlukIsYuku = 1 },
    new Usta { Id = 2, Ad = "Ayşe Usta",   Uzmanlik = "Elektrikçi", Puan = 4.9, GunlukIsYuku = 0 },
    new Usta { Id = 3, Ad = "Ali Usta",    Uzmanlik = "Genel",      Puan = 4.4, GunlukIsYuku = 2 },
};

var ustaDepo = new BellekUstaDeposu(ustalar);
var isEmriDepo = new BellekIsEmriDeposu();
var eslestirme = new BasitEslestirmeServisi();
var zamanlayici = new Zamanlayici();

var vatandas = new Vatandas { Id = 1, Ad = "Efecan Çadırcı" };
var talep = vatandas.TalepOlustur("Sızıntı tamiri – Tesisatçı lazım", DateTime.Now.AddHours(2), "Arcadia/Merkez, 5. Sokak");

var usta = eslestirme.Eslestir(talep, ustaDepo.TumunuGetir());

// Fiyat motoru
var motor = new FiyatMotoru(new FiyatKurali[] { new HaftaSonuEkUcretiKurali(), new AcilCagriUcretiKurali() });
var eklentiKlasoru = System.IO.Path.Combine(AppContext.BaseDirectory, "Plugins");
motor.EklentileriYukle(eklentiKlasoru);

decimal taban = 200m;
var (sonFiyat, uygulanan) = motor.Hesapla(taban);

var emri = new IsEmri { Id = 1, Talep = talep, Usta = usta, Baslangic = DateTime.Now.AddHours(2), Fiyat = sonFiyat };
isEmriDepo.Ekle(emri);

zamanlayici.Ata(emri, (X: 10, Y: 6));

Console.WriteLine($"Vatandaş: {vatandas.Ad}");
Console.WriteLine($"Atanan Usta: {usta.Ad} ({usta.Uzmanlik})");
Console.WriteLine($"Fiyat: {ParaYazici.Yaz(sonFiyat)}");
Console.WriteLine("Uygulanan Kurallar: " + (uygulanan.Count == 0 ? "(yok)" : string.Join(", ", uygulanan)));