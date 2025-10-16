// ==== ÇOKLU VATANDAŞ SENARYOSU ====
// 1) Ustalar (varsa mevcut listen kalsın)
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

// 2) Vatandaş listesi
var vatandaslar = new List<Vatandas>
{
    new Vatandas { Id = 1, Ad = "Ali Veli" },
    new Vatandas { Id = 2, Ad = "Ayça Demir" },
    new Vatandas { Id = 3, Ad = "Mert Kaya" },
    new Vatandas { Id = 4, Ad = "Zeynep Yıldız" },
};

// 3) Talepler (kim, ne, nerede, taban fiyat)
var talepler = new (Vatandas K, string Aciklama, string Adres, decimal Taban, (int X, int Y) Nokta)[]
{
    (vatandaslar[0], "Sızıntı tamiri – Tesisatçı lazım", "Merkez Mah. 5. Sk.", 200m, (10,6)),
    (vatandaslar[1], "Sigorta atıyor – Elektrikçi lazım", "Atatürk Cd. 12/3",   250m, (12,3)),
    (vatandaslar[2], "Musluk değişimi – Tesisatçı",       "Kaplan Sk. 7",       180m, (9,8)),
    (vatandaslar[3], "Avize montajı – Elektrikçi",        "Papatya Sk. 2",      220m, (11,4)),
};

// 4) Fiyat motoru (kuralları ve eklentileri 1 kez yükle)
var motor = new FiyatMotoru(new FiyatKurali[] { new HaftaSonuEkUcretiKurali(), new AcilCagriUcretiKurali() });
var eklentiKlasoru = Path.Combine(AppContext.BaseDirectory, "Plugins");
motor.EklentileriYukle(eklentiKlasoru);

// 5) Her talebi işle
int idSayac = 1;
foreach (var t in talepler)
{
    var talep = t.K.TalepOlustur(t.Aciklama, DateTime.Now.AddHours(2), t.Adres);

    // uygun usta
    var usta = eslestirme.Eslestir(talep, ustaDepo.TumunuGetir());

    // fiyatla
    var (sonFiyat, uygulanan) = motor.Hesapla(t.Taban);

    // iş emri
    var emri = new IsEmri
    {
        Id = idSayac++,
        Talep = talep,
        Usta = usta,
        Baslangic = DateTime.Now.AddHours(2),
        Fiyat = sonFiyat
    };
    isEmriDepo.Ekle(emri);

    // çizelge + rota
    zamanlayici.Ata(emri, t.Nokta);

    // çıktı
    Console.WriteLine("— — — — —");
    Console.WriteLine($"Vatandaş: {t.K.Ad}");
    Console.WriteLine($"Talep   : {talep.Aciklama}");
    Console.WriteLine($"Adres   : {talep.Adres}");
    Console.WriteLine($"Usta    : {usta.Ad} ({usta.Uzmanlik})");
    Console.WriteLine($"Fiyat   : {ParaYazici.Yaz(sonFiyat)}");  // örn: 230,00 TL
    Console.WriteLine("Kurallar: " + (uygulanan.Count == 0 ? "(yok)" : string.Join(", ", uygulanan)));
}

// Gün özeti
var bugun = DateOnly.FromDateTime(DateTime.Now);
Console.WriteLine("\n=== Gün Özeti ===");
Console.WriteLine($"Bugünkü iş sayısı: {zamanlayici.Cizelge[bugun].Count}");
Console.WriteLine("Rota noktaları:");
foreach (var p in zamanlayici.Rota) Console.WriteLine($"  → ({p.X},{p.Y})");
