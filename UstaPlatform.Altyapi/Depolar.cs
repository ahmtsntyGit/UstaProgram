using System.Linq;          // ÖNEMLİ: Where, ToList
using UstaPlatform.Alan;

namespace UstaPlatform.Altyapi;

public sealed class BellekIsEmriDeposu : IIsEmriDeposu
{
    private readonly List<IsEmri> _liste = new();

    public void Ekle(IsEmri emri) => _liste.Add(emri);

    public IReadOnlyList<IsEmri> GuneGoreGetir(DateOnly gun)
        => _liste.Where(x => DateOnly.FromDateTime(x.Baslangic) == gun).ToList();
}

public sealed class BellekUstaDeposu : IUstaDeposu
{
    private readonly List<Usta> _ustalar;
    public BellekUstaDeposu(IEnumerable<Usta> tohum) => _ustalar = tohum.ToList();
    public IReadOnlyList<Usta> TumunuGetir() => _ustalar;
}