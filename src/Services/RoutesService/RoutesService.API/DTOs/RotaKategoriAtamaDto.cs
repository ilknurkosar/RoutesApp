namespace RoutesService.API.DTOs;
public class RotaKategoriAtamaListDto
{
    public int Id { get; set; }
    public int RotaId { get; set; }
    public int KategoriId { get; set; }
    public string? RotaAdi { get; set; }
    public string? KategoriAd { get; set; }
}

public class RotaKategoriAtamaDetailDto
{
    public int Id { get; set; }
    public int RotaId { get; set; }
    public int KategoriId { get; set; }
    public string? RotaAdi { get; set; }
    public string? KategoriAd { get; set; }
}

public class RotaKategoriAtamaCreateDto
{
    public int RotaId { get; set; }
    public int KategoriId { get; set; }
}

public class RotaKategoriAtamaUpdateDto
{
    public int RotaId { get; set; }
    public int KategoriId { get; set; }
}

