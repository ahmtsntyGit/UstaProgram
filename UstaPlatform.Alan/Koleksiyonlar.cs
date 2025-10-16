using System.Collections;

namespace UstaPlatform.Alan;

// Rota: IEnumerable<(int X,int Y)> + koleksiyon başlatıcı desteği
public sealed class Rota : IEnumerable<(int X, int Y)>
{
    private readonly List<(int X, int Y)> _noktalar = new();

    // Koleksiyon başlatıcı için "Add" adı şart
    public void Add(int x, int y) => _noktalar.Add((x, y));

    // Türkçe kullanan kodlar için eş-anlamlı
    public void Ekle(int x, int y) => Add(x, y);

    public IEnumerator<(int X, int Y)> GetEnumerator() => _noktalar.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

// Çizelge: gün -> iş emri listesi (indexer)
public sealed class Cizelge
{
    private readonly Dictionary<DateOnly, List<IsEmri>> _harita = new();

    public List<IsEmri> this[DateOnly gun]
    {
        get
        {
            if (!_harita.TryGetValue(gun, out var liste))
            {
                liste = new List<IsEmri>();
                _harita[gun] = liste;
            }
            return liste;
        }
    }
}
