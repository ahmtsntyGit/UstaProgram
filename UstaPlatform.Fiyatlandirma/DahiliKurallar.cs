namespace UstaPlatform.Fiyatlandirma;

public sealed class HaftaSonuEkUcretiKurali : FiyatKurali
{
    public string Ad => "Hafta Sonu Ek Ücreti";
    public decimal Uygula(decimal tabanFiyat)
        => (DateTime.Now.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            ? tabanFiyat * 1.15m : tabanFiyat;
}

public sealed class AcilCagriUcretiKurali : FiyatKurali
{
    public string Ad => "Acil Çağrı Ücreti";
    public decimal Uygula(decimal tabanFiyat)
        => tabanFiyat < 200 ? tabanFiyat + 50 : tabanFiyat;
}