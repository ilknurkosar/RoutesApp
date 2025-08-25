namespace RoutesService.API.DTOs;

public record KullaniciListDto(int Id, string? Ad, string? Email);
public record KullaniciDetailDto(int Id, string? Ad, string? Soyad, string? KullaniciAdi, string? Email, string? Telefon);

public class KullaniciCreateDto
{
    public string? Ad { get; set; }
    public string? Soyad { get; set; }
    public string? KullaniciAdi { get; set; }
    public string? Email { get; set; }
    public string? Sifre { get; set; }
    public string? Telefon { get; set; }
}
public class KullaniciUpdateDto
{
    public string? Ad { get; set; }
    public string? Soyad { get; set; }
    public string? KullaniciAdi { get; set; }
    public string? Email { get; set; }
    public string? Telefon { get; set; }
}
