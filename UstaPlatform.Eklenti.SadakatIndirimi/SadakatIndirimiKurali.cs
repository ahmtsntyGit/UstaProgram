using UstaPlatform.Fiyatlandirma;

namespace UstaPlatform.Eklenti.SadakatIndirimi;

// 250 TL üzeri %10 indirim (demo)
public sealed class SadakatIndirimiKurali : FiyatKurali
{
    public string Ad => "Sadakat İndirimi";
    public decimal Uygula(decimal tabanFiyat)
        => tabanFiyat >= 250m ? tabanFiyat * 0.90m : tabanFiyat;
}