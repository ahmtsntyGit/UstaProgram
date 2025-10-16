using UstaPlatform.Alan;

namespace UstaPlatform.Altyapi;

public sealed class Zamanlayici
{
    public Cizelge Cizelge { get; } = new();
    public Rota Rota { get; } = new();

    public void Ata(IsEmri emri, (int X, int Y) adresKoord)
    {
        var gun = DateOnly.FromDateTime(emri.Baslangic);
        Cizelge[gun].Add(emri);
        Rota.Ekle(adresKoord.X, adresKoord.Y);
        emri.Usta.GunlukIsYuku++;
    }
}