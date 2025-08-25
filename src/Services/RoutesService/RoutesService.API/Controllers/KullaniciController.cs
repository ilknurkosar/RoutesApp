using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoutesService.API.Data;
using RoutesService.API.DTOs;
using RoutesService.Domain.Entities;

namespace RoutesService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : ControllerBase
    {
        private readonly RoutesDbContext _db;
        private readonly IMapper _mapper;

        public KullaniciController(RoutesDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        // LIST
        [HttpGet]
        public async Task<IEnumerable<KullaniciListDto>> GetKullanicilar()
            => await _db.Kullanicilar
                .AsNoTracking()
                .ProjectTo<KullaniciListDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

        // DETAIL
        [HttpGet("{id:int}")]
        public async Task<ActionResult<KullaniciDetailDto>> GetKullanici(int id)
        {
            var dto = await _db.Kullanicilar.AsNoTracking()
                .Where(x => x.Id == id)
                .ProjectTo<KullaniciDetailDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (dto is null) return NotFound();
            return dto;
        }

        // CREATE (register)
        [HttpPost]
        public async Task<ActionResult<KullaniciDetailDto>> PostKullanici([FromBody] KullaniciCreateDto dto)
        {

            if (await _db.Kullanicilar.AnyAsync(u => u.Email == dto.Email))
                return Conflict("Bu e‑posta zaten kayıtlı.");
            if (await _db.Kullanicilar.AnyAsync(u => u.KullaniciAdi == dto.KullaniciAdi))
                return Conflict("Bu kullanıcı adı zaten kayıtlı.");

            var ent = _mapper.Map<Kullanici>(dto);

            ent.Sifre = BCrypt.Net.BCrypt.HashPassword(dto.Sifre);

            _db.Kullanicilar.Add(ent);
            await _db.SaveChangesAsync();

            var detail = _mapper.Map<KullaniciDetailDto>(ent);
            return CreatedAtAction(nameof(GetKullanici), new { id = ent.Id }, detail);
        }

        // UPDATE
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutKullanici(int id, [FromBody] KullaniciUpdateDto dto)
        {
            var ent = await _db.Kullanicilar.FirstOrDefaultAsync(x => x.Id == id);
            if (ent is null) return NotFound();


            if (!string.IsNullOrWhiteSpace(dto.Email) && dto.Email != ent.Email)
            {
                if (await _db.Kullanicilar.AnyAsync(u => u.Email == dto.Email && u.Id != id))
                    return Conflict("Bu e‑posta başka bir kullanıcıda mevcut.");
            }
            if (!string.IsNullOrWhiteSpace(dto.KullaniciAdi) && dto.KullaniciAdi != ent.KullaniciAdi)
            {
                if (await _db.Kullanicilar.AnyAsync(u => u.KullaniciAdi == dto.KullaniciAdi && u.Id != id))
                    return Conflict("Bu kullanıcı adı başka bir kullanıcıda mevcut.");
            }

            _mapper.Map(dto, ent);


            if (dto.Sifre != null)
            {
                if (string.IsNullOrWhiteSpace(dto.Sifre))
                    return BadRequest("Şifre boş olamaz.");
                ent.Sifre = BCrypt.Net.BCrypt.HashPassword(dto.Sifre);
            }

            await _db.SaveChangesAsync();
            return NoContent();
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteKullanici(int id)
        {
            var ent = await _db.Kullanicilar.FindAsync(id);
            if (ent is null) return NotFound();

            _db.Kullanicilar.Remove(ent);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
