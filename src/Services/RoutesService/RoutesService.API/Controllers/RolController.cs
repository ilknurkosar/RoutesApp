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
    public class RolController : ControllerBase
    {
        private readonly RoutesDbContext _db;
        private readonly IMapper _mapper;

        public RolController(RoutesDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        // GET: api/Rol
        [HttpGet]
        public async Task<IEnumerable<RolListDto>> GetRoller()
            => await _db.Roller
                .AsNoTracking()
                .ProjectTo<RolListDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

        // GET: api/Rol/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RolDetailDto>> GetRol(int id)
        {
            var dto = await _db.Roller.AsNoTracking()
                .Where(r => r.Id == id)
                .ProjectTo<RolDetailDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (dto is null) return NotFound();
            return dto;
        }

        // POST: api/Rol
        [Authorize(Roles = "Admin")] // rol yaratma yetkisini kısıtla (opsiyonel)
        [HttpPost]
        public async Task<ActionResult<RolDetailDto>> PostRol([FromBody] RolCreateDto dto)
        {
            // basit benzersizlik kontrolü
            if (await _db.Roller.AnyAsync(r => r.Ad == dto.Ad))
                return Conflict("Bu rol adı zaten mevcut.");

            var ent = _mapper.Map<Rol>(dto);
            _db.Roller.Add(ent);
            await _db.SaveChangesAsync();

            var detail = _mapper.Map<RolDetailDto>(ent);
            return CreatedAtAction(nameof(GetRol), new { id = ent.Id }, detail);
        }

        // PUT: api/Rol/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutRol(int id, [FromBody] RolUpdateDto dto)
        {
            var ent = await _db.Roller.FirstOrDefaultAsync(r => r.Id == id);
            if (ent is null) return NotFound();

            if (await _db.Roller.AnyAsync(r => r.Ad == dto.Ad && r.Id != id))
                return Conflict("Bu rol adı başka bir kayıtta mevcut.");

            _mapper.Map(dto, ent);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Rol/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            var ent = await _db.Roller.FindAsync(id);
            if (ent is null) return NotFound();

            _db.Roller.Remove(ent);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
