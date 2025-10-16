namespace UstaPlatform.Alan;

public interface IIsEmriDeposu
{
    void Ekle(IsEmri emri);
    IReadOnlyList<IsEmri> GuneGoreGetir(DateOnly gun);
}

public interface IUstaDeposu
{
    IReadOnlyList<Usta> TumunuGetir();
}

public interface IEslestirmeServisi
{
    Usta Eslestir(Talep talep, IReadOnlyList<Usta> ustalar);
}
