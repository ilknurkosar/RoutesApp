using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoutesService.API.Data;
using RoutesService.API.DTOs;
using RoutesService.API.DTOs;
using RoutesService.Domain.Entities;

namespace RoutesService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class KullaniciRolleriController : ControllerBase
    {
        private readonly RoutesDbContext _db;
        private readonly IMapper _mapper;
        private readonly RoutesDbContext _db;
        private readonly IMapper _mapper;

        public KullaniciRolleriController(RoutesDbContext db, IMapper mapper)
        public KullaniciRolleriController(RoutesDbContext db, IMapper mapper)
        {
            _db = db; _mapper = mapper;
        }

        // LIST
        // GET: api/KullaniciRolleri
        [HttpGet]
        public async Task<IEnumerable<KullaniciRolListDto>> Get()
            => await _db.KullaniciRolleri
                .AsNoTracking()
                .ProjectTo<KullaniciRolListDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

        // DETAIL
        // GET: api/KullaniciRolleri/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<KullaniciRolDetailDto>> Get(int id)
        {
            var dto = await _db.KullaniciRolleri.AsNoTracking()
                .Where(x => x.Id == id)
                .ProjectTo<KullaniciRolDetailDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (dto is null) return NotFound();
            return dto;
        }

        // CREATE
        // POST: api/KullaniciRolleri
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<KullaniciRolDetailDto>> Create([FromBody] KullaniciRolCreateDto dto)
        {
            // FK var mı?
            if (!await _db.Kullanicilar.AnyAsync(x => x.Id == dto.KullaniciId))
                return BadRequest("Geçersiz KullaniciId.");
            if (!await _db.Roller.AnyAsync(x => x.Id == dto.RolId))
                return BadRequest("Geçersiz RolId.");

            // (KullaniciId, RolId) çifti zaten var mı? (tekillik)
            var exists = await _db.KullaniciRolleri
                .AnyAsync(x => x.KullaniciId == dto.KullaniciId && x.RolId == dto.RolId);
            if (exists) return Conflict("Bu kullanıcıya bu rol zaten atanmış.");

            var ent = _mapper.Map<KullaniciRolleri>(dto);
            _db.KullaniciRolleri.Add(ent);
            await _db.SaveChangesAsync();

            // Detay DTO oluşturmak için tekrar çekmek yerine map edebiliriz,
            // ama KullaniciAd/RolAd için navigation gerekirse AsNoTracking çekebilirsin:
            var detail = await _db.KullaniciRolleri.AsNoTracking()
                .Where(x => x.Id == ent.Id)
                .ProjectTo<KullaniciRolDetailDto>(_mapper.ConfigurationProvider)
                .FirstAsync();

            return CreatedAtAction(nameof(Get), new { id = ent.Id }, detail);
        }

        // UPDATE
        // PUT: api/KullaniciRolleri/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] KullaniciRolUpdateDto dto)
        {
            var ent = await _db.KullaniciRolleri.FirstOrDefaultAsync(x => x.Id == id);
            if (ent is null) return NotFound();

            if (!await _db.Kullanicilar.AnyAsync(x => x.Id == dto.KullaniciId))
                return BadRequest("Geçersiz KullaniciId.");
            if (!await _db.Roller.AnyAsync(x => x.Id == dto.RolId))
                return BadRequest("Geçersiz RolId.");

            // aynı çifte güncellenmek isteniyorsa çakışma kontrolü
            var duplicate = await _db.KullaniciRolleri
                .AnyAsync(x => x.KullaniciId == dto.KullaniciId &&
                               x.RolId == dto.RolId &&
                               x.Id != id);
            if (duplicate) return Conflict("Bu kullanıcıya bu rol zaten atanmış.");

            _mapper.Map(dto, ent);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE
        // DELETE: api/KullaniciRolleri/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ent = await _db.KullaniciRolleri.FindAsync(id);
            if (ent is null) return NotFound();

            _db.KullaniciRolleri.Remove(ent);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // ---- İsteğe bağlı yardımcı endpoint'ler ----

        // Belirli kullanıcının rollerini listele
        // GET: api/KullaniciRolleri/user/2
        [HttpGet("user/{kullaniciId:int}")]
        public async Task<IEnumerable<KullaniciRolListDto>> GetByUser(int kullaniciId)
            => await _db.KullaniciRolleri.AsNoTracking()
                .Where(x => x.KullaniciId == kullaniciId)
                .ProjectTo<KullaniciRolListDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

        // Belirli role sahip kullanıcıları listele
        // GET: api/KullaniciRolleri/role/1
        [HttpGet("role/{rolId:int}")]
        public async Task<IEnumerable<KullaniciRolListDto>> GetByRole(int rolId)
            => await _db.KullaniciRolleri.AsNoTracking()
                .Where(x => x.RolId == rolId)
                .ProjectTo<KullaniciRolListDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
    }
}
