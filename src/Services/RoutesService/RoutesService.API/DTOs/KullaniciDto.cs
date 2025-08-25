namespace RoutesService.API.DTOs;

public class KullaniciListDto
{
    public int Id { get; set; }
    public string? Ad { get; set; }
    public string? Soyad { get; set; }
    public string? Email { get; set; }
    public string? Telefon { get; set; }
}

public class KullaniciDetailDto
{
    public int Id { get; set; }
    public string? Ad { get; set; }
    public string? Soyad { get; set; }
    public string? Email { get; set; }
    public string? Telefon { get; set; }
}

// Kayıt (register/create)
public class KullaniciCreateDto
{
    public string Ad { get; set; } = null!;
    public string? Soyad { get; set; }
    public string KullaniciAdi { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Sifre { get; set; } = null!;
    public string? Telefon { get; set; }
}

// Güncelleme
public class KullaniciUpdateDto
{
    public string? Ad { get; set; }
    public string? Soyad { get; set; }
    public string? KullaniciAdi { get; set; }
    public string? Email { get; set; }
    public string? Telefon { get; set; }

    // Opsiyonel şifre değişimi: null → dokunma, "" → reddet, değer → hashle
    public string? Sifre { get; set; }
}
