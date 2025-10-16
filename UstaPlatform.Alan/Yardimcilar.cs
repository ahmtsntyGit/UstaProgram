namespace UstaPlatform.Alan;
using System.Globalization;
using System.Text;

public static class Kontrol
{
    public static void NullDegil<T>(T? nesne, string ad) where T : class
    {
        if (nesne is null) throw new ArgumentNullException(ad);
    }
}

public static class ParaYazici
{
    private static readonly CultureInfo Tr = CultureInfo.GetCultureInfo("tr-TR");

    // (İstersen tut: konsolda Türkçe karakterler için)
    public static void KonsoluHazirla() => Console.OutputEncoding = Encoding.UTF8;

    // Para birimi işareti YOK: 12.345,67 TL şeklinde yazdırır
    public static string Yaz(decimal tutar)
    {
        // "N2" → 2 ondalık, tr-TR biçimlendirme, para işareti yok
        return string.Format(Tr, "{0:N2} TL", tutar);
    }

    public static class KonumYardimcisi
    {
        // Demo amaçlı: manhattan uzaklığı
        public static int Uzaklik((int X, int Y) a, (int X, int Y) b) => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }
}
