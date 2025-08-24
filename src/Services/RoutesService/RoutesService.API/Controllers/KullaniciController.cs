using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using RoutesService.API.Data;
using RoutesService.API.DTOs;
using RoutesService.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class KullaniciController : ControllerBase
{
    private readonly RoutesDbContext _db;

    public KullaniciController(RoutesDbContext db)
    {
        _db = db;
    }

    // 🔹 Tüm kullanıcıları listeleme → Sadece Admin
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _db.Kullanicilar
            .Include(k => k.KullaniciRolleri)!.ThenInclude(kr => kr.Rol)
            .ToListAsync();

        return Ok(users.Select(u => new {
            u.Id,
            u.KullaniciAdi,
            u.Email,
            Roller = u.KullaniciRolleri.Select(r => r.Rol!.Ad).ToList()
        }));
    }

    // 🔹 Tek kullanıcı bilgisi → Hem User hem Admin kendi bilgisine erişebilir
    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _db.Kullanicilar
            .Include(k => k.KullaniciRolleri)!.ThenInclude(kr => kr.Rol)
            .FirstOrDefaultAsync(k => k.Id == id);

        if (user == null) return NotFound();

        if (!User.IsInRole("Admin") &&
            User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value != id.ToString())
        {
            return Forbid();
        }

        return Ok(new
        {
            user.Id,
            user.KullaniciAdi,
            user.Email,
            Roller = user.KullaniciRolleri.Select(r => r.Rol!.Ad).ToList()
        });
    }

    // 🔹 Yeni kullanıcı oluşturma → Admin yapabilir, default rol = User
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] KullaniciCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Ad) ||
            string.IsNullOrWhiteSpace(dto.Soyad) ||
            string.IsNullOrWhiteSpace(dto.KullaniciAdi) ||
            string.IsNullOrWhiteSpace(dto.Email) ||
            string.IsNullOrWhiteSpace(dto.Sifre))
        {
            return BadRequest("Tüm alanlar doldurulmalıdır.");
        }

        var user = new Kullanici
        {
            Ad = dto.Ad,
            Soyad = dto.Soyad,
            KullaniciAdi = dto.KullaniciAdi,
            Email = dto.Email,
            Sifre = BCrypt.Net.BCrypt.HashPassword(dto.Sifre)
        };

        _db.Kullanicilar.Add(user);
        await _db.SaveChangesAsync();

        // 🔹 Varsayılan User rolü
        var userRole = await _db.Roller.FirstOrDefaultAsync(r => r.Ad == "User");
        if (userRole != null)
        {
            _db.KullaniciRolleri.Add(new KullaniciRolleri
            {
                KullaniciId = user.Id,
                RolId = userRole.Id
            });
            await _db.SaveChangesAsync();
        }

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, new { user.Id, user.KullaniciAdi });
    }

    // 🔹 Kullanıcı güncelleme → kendi veya admin
    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, [FromBody] KullaniciUpdateDto dto)
    {
        var user = await _db.Kullanicilar.FindAsync(id);
        if (user == null) return NotFound();

        if (!User.IsInRole("Admin") &&
            User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value != id.ToString())
        {
            return Forbid();
        }

        if (!string.IsNullOrWhiteSpace(dto.KullaniciAdi)) user.KullaniciAdi = dto.KullaniciAdi;
        if (!string.IsNullOrWhiteSpace(dto.Email)) user.Email = dto.Email;
        if (!string.IsNullOrWhiteSpace(dto.Sifre))
            user.Sifre = BCrypt.Net.BCrypt.HashPassword(dto.Sifre);

        await _db.SaveChangesAsync();
        return NoContent();
    }

    // 🔹 Kullanıcı silme → Sadece Admin
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _db.Kullanicilar.FindAsync(id);
        if (user == null) return NotFound();

        _db.Kullanicilar.Remove(user);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
