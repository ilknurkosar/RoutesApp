using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using RoutesService.API.Data;
using RoutesService.API.DTOs;
using RoutesService.Domain.Entities;
using static RoutesService.API.DTOs.AuthDtos;

namespace RoutesService.API.Controllers
{
    public class AuthController : ControllerBase
    {

        private readonly RoutesDbContext _db;
        private readonly ITokenService _tokenService;

        public AuthController(RoutesDbContext db, ITokenService tokenService)
        {
            _db = db;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.KullaniciAdiOrEmail) || string.IsNullOrWhiteSpace(dto.Sifre))
                return Unauthorized("Kullanıcı adı/e-posta ve şifre zorunludur.");

            var user = await _db.Kullanicilar
                .Include(k => k.KullaniciRolleri)!.ThenInclude(kr => kr.Rol)
                .FirstOrDefaultAsync(k =>
                    k.KullaniciAdi == dto.KullaniciAdiOrEmail || k.Email == dto.KullaniciAdiOrEmail);

            if (user is null) return Unauthorized("Kullanıcı bulunamadı.");

            bool passwordOk = false;
            try
            {
                passwordOk = BCrypt.Net.BCrypt.Verify(dto.Sifre, user.Sifre);
            }
            catch
            {
                passwordOk = (user.Sifre == dto.Sifre);
            }

            if (!passwordOk) return Unauthorized("Hatalı şifre.");

            var token = await _tokenService.CreateTokenAsync(user.Id);

            var roles = user.KullaniciRolleri?
                .Where(x => x.Rol != null && !string.IsNullOrWhiteSpace(x.Rol.Ad))
                .Select(x => x.Rol!.Ad!)
                .Distinct()
                .ToList() ?? new List<string>();

            var res = new LoginResponseDto
            {
                UserId = user.Id,
                KullaniciAdi = user.KullaniciAdi,
                Token = token,
                Roles = roles
            };

            return Ok(res);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] DTOs.KullaniciCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Ad) ||
                string.IsNullOrWhiteSpace(dto.Soyad) ||
                string.IsNullOrWhiteSpace(dto.KullaniciAdi) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Sifre))
            {
                return BadRequest("Tüm alanlar doldurulmalıdır.");
            }

            var exists = await _db.Kullanicilar
                .AnyAsync(u => u.KullaniciAdi == dto.KullaniciAdi || u.Email == dto.Email);
            if (exists) return Conflict("Bu kullanıcı adı veya e-posta zaten kayıtlı.");

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

            // 🔹 Varsayılan User rolü ata
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

            return Ok(new { user.Id, user.KullaniciAdi, user.Email });
        }




    }
}
