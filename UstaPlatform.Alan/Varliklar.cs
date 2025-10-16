namespace UstaPlatform.Alan;

public sealed class Usta
{
    public int Id { get; set; }
    public string Ad { get; set; } = string.Empty;
    public string Uzmanlik { get; set; } = string.Empty;
    public double Puan { get; set; } = 5.0;
    public int GunlukIsYuku { get; set; }
}

public sealed class Vatandas
{
    public int Id { get; set; }
    public string Ad { get; set; } = string.Empty;

    public Talep TalepOlustur(string aciklama, DateTime tarih, string adres)
        => new Talep { Vatandas = this, Aciklama = aciklama, Tarih = tarih, Adres = adres };
}

public sealed class Talep
{
    public int Id { get; set; }
    public Vatandas? Vatandas { get; set; }
    public string Aciklama { get; set; } = string.Empty;
    public DateTime Tarih { get; set; } = DateTime.Now;
    public string Adres { get; set; } = string.Empty;
}

public sealed class IsEmri
{
    public int Id { get; set; }
    public Talep Talep { get; set; } = default!;
    public Usta Usta { get; set; } = default!;
    public DateTime Baslangic { get; set; }
    public decimal Fiyat { get; set; }
}