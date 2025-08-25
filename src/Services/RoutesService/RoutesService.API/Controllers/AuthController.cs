using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoutesService.API.Data;
using RoutesService.API.DTOs;
using RoutesService.API.Services;
using RoutesService.Domain.Entities;

namespace RoutesService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly RoutesDbContext _db;
    private readonly IJwtTokenService _token;
    private readonly IConfiguration _cfg;

    public AuthController(RoutesDbContext db, IJwtTokenService token, IConfiguration cfg)
    {
        _db = db; _token = token; _cfg = cfg;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginResponseDto), 200)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        var user = await _db.Kullanicilar.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user is null) return Unauthorized("E‑posta veya şifre hatalı.");

        // ŞİFRE DOĞRULAMA — hash kullandığını varsayıyorum
        if (!BCrypt.Net.BCrypt.Verify(dto.Sifre, user.Sifre))
            return Unauthorized("E‑posta veya şifre hatalı.");

        var roles = await _db.KullaniciRolleri
            .Include(kr => kr.Rol)
            .Where(kr => kr.KullaniciId == user.Id)
            .Select(kr => kr.Rol.Ad)   
            .ToArrayAsync();

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(ClaimTypes.Name, user.Ad ?? string.Empty)
        };
        foreach (var r in roles) claims.Add(new Claim(ClaimTypes.Role, r));

        var minutes = int.Parse(_cfg["Jwt:ExpireMinutes"] ?? "60");
        var expiresAt = DateTime.UtcNow.AddMinutes(minutes);
        var tokenStr = _token.CreateToken(claims, expiresAt);

        return Ok(new LoginResponseDto(
            AccessToken: tokenStr,
            ExpiresAtUtc: expiresAt,
            KullaniciId: user.Id,
            Ad: user.Ad ?? string.Empty,
            Email: user.Email ?? string.Empty,
            Roller: roles
        ));
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
    {
        if (await _db.Kullanicilar.AnyAsync(x => x.Email == dto.Email))
            return Conflict("Bu e‑posta zaten kayıtlı.");

        var user = new Kullanici
        {
            Ad = dto.Ad,
            Email = dto.Email,
            Sifre = BCrypt.Net.BCrypt.HashPassword(dto.Sifre) // 🔐 HASH
        };

        _db.Kullanicilar.Add(user);
        await _db.SaveChangesAsync();
        return Created(string.Empty, new { message = "Kayıt başarılı. Şimdi login olabilirsiniz." });
    }

}
