using System.Reflection;
using System.Linq;   // ÖNEMLİ: Reflection filtresi ve LINQ

namespace UstaPlatform.Fiyatlandirma;

public sealed class FiyatMotoru
{
    private readonly List<FiyatKurali> _kurallar = new();

    public FiyatMotoru(IEnumerable<FiyatKurali>? dahililer = null)
    {
        if (dahililer != null) _kurallar.AddRange(dahililer);
    }

    public void EklentileriYukle(string klasor)
    {
        if (!Directory.Exists(klasor)) return;

        foreach (var dll in Directory.EnumerateFiles(klasor, "*.dll"))
        {
            try
            {
                var asm = Assembly.LoadFrom(dll);
                var tipler = asm.GetTypes()
                    .Where(t => !t.IsAbstract && typeof(FiyatKurali).IsAssignableFrom(t));

                foreach (var t in tipler)
                {
                    if (Activator.CreateInstance(t) is FiyatKurali kural)
                        _kurallar.Add(kural);
                }
            }
            catch
            {
                // loglamak istersen buraya yaz
            }
        }
    }

    public (decimal fiyat, List<string> uygulanan) Hesapla(decimal tabanFiyat)
    {
        decimal fiyat = tabanFiyat;
        var uygulanan = new List<string>();

        foreach (var k in _kurallar)
        {
            var eski = fiyat;
            fiyat = k.Uygula(fiyat);
            if (fiyat != eski) uygulanan.Add(k.Ad);
        }

        return (fiyat, uygulanan);
    }
}