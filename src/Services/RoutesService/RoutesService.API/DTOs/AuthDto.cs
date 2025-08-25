namespace RoutesService.API.DTOs;

public class RegisterRequestDto
{
    public string Ad { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Sifre { get; set; } = null!;
}


public class LoginRequestDto
{
    public string Email { get; set; } = null!;
    public string Sifre { get; set; } = null!;
}

public record LoginResponseDto(
    string AccessToken,
    DateTime ExpiresAtUtc,
    int KullaniciId,
    string Ad,
    string Email,
    string[] Roller
);
