namespace UstaPlatform.Fiyatlandirma;

public interface FiyatKurali
{
    string Ad { get; }
    decimal Uygula(decimal tabanFiyat);
}