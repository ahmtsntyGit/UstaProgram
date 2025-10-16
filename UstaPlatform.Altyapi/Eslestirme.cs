using System.Linq;          // ÖNEMLİ: Where, ToList, MinBy
using UstaPlatform.Alan;

namespace UstaPlatform.Altyapi;

public sealed class BasitEslestirmeServisi : IEslestirmeServisi
{
    public Usta Eslestir(Talep talep, IReadOnlyList<Usta> ustalar)
    {
        // basit kural: uzmanlık uyanlar arasından en az yükte olan
        var adaylar = ustalar
            .Where(u => u.Uzmanlik.Equals("Genel", StringComparison.OrdinalIgnoreCase)
                        || (talep.Aciklama?.Contains(u.Uzmanlik, StringComparison.OrdinalIgnoreCase) ?? false))
            .ToList();

        if (adaylar.Count == 0) adaylar = ustalar.ToList();
        return adaylar.MinBy(u => u.GunlukIsYuku)!; // .NET 6+ LINQ eklentisi
    }
}