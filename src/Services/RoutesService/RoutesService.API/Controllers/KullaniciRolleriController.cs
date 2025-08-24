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
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class KullaniciRolleriController : ControllerBase
    {
        private readonly RoutesDbContext _db;
        private readonly IMapper _mapper;

        public KullaniciRolleriController(RoutesDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<KullaniciRolleriListDto>> Get()
        {
            return await _db.KullaniciRolleri
                            .AsNoTracking()
                            .Include(x => x.Kullanici)
                            .Include(x => x.Rol)
                            .ProjectTo<KullaniciRolleriListDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<KullaniciRolleriDetailDto>> Get(int id)
        {
            var ent = await _db.KullaniciRolleri
                               .AsNoTracking()
                               .Include(x => x.Kullanici)
                               .Include(x => x.Rol)
                               .FirstOrDefaultAsync(x => x.Id == id);

            if (ent == null) return NotFound();
            return _mapper.Map<KullaniciRolleriDetailDto>(ent);
        }

        [HttpPost]
        public async Task<ActionResult<KullaniciRolleriDetailDto>> Create(KullaniciRolleriCreateDto dto)
        {
            var ent = _mapper.Map<KullaniciRolleri>(dto);
            _db.KullaniciRolleri.Add(ent);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = ent.Id }, _mapper.Map<KullaniciRolleriDetailDto>(ent));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, KullaniciRolleriUpdateDto dto)
        {
            var ent = await _db.KullaniciRolleri.FirstOrDefaultAsync(x => x.Id == id);
            if (ent == null) return NotFound();

            _mapper.Map(dto, ent);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ent = await _db.KullaniciRolleri.FindAsync(id);
            if (ent == null) return NotFound();

            _db.KullaniciRolleri.Remove(ent);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
