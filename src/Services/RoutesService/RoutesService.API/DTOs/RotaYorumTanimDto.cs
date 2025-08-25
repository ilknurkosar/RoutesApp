namespace RoutesService.API.DTOs;

public class RotaYorumTanimListDto
{
    public int Id { get; set; }
    public int? RotaId { get; set; }
    public int? KullaniciId { get; set; }
    public int? Puan { get; set; }
    public string? Yorum { get; set; }
    public string? RotaAdi { get; set; }
    public string? KullaniciAd { get; set; }
}

public class RotaYorumTanimDetailDto
{
    public int Id { get; set; }
    public int? RotaId { get; set; }
    public int? KullaniciId { get; set; }
    public int? Puan { get; set; }
    public string? Yorum { get; set; }
    public string? RotaAdi { get; set; }
    public string? KullaniciAd { get; set; }
}

public class RotaYorumTanimCreateDto
{
    public int? RotaId { get; set; }
    public int? KullaniciId { get; set; }
    public int? Puan { get; set; }
    public string? Yorum { get; set; }
}

public class RotaYorumTanimUpdateDto
{
    public int? RotaId { get; set; }
    public int? KullaniciId { get; set; }
    public int? Puan { get; set; }
    public string? Yorum { get; set; }
}