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
    public class RolIzinleriController : ControllerBase
    {
        private readonly RoutesDbContext _db;
        private readonly IMapper _mapper;

        public RolIzinleriController(RoutesDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<RolIzinleriListDto>> Get()
        {
            return await _db.RolIzinleri
                            .AsNoTracking()
                            .Include(x => x.Rol)
                            .Include(x => x.Izin)
                            .ProjectTo<RolIzinleriListDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RolIzinleriDetailDto>> Get(int id)
        {
            var ent = await _db.RolIzinleri
                               .AsNoTracking()
                               .Include(x => x.Rol)
                               .Include(x => x.Izin)
                               .FirstOrDefaultAsync(x => x.Id == id);
            if (ent == null) return NotFound();
            return _mapper.Map<RolIzinleriDetailDto>(ent);
        }

        [HttpPost]
        public async Task<ActionResult<RolIzinleriDetailDto>> Create(RolIzinleriCreateDto dto)
        {
            var ent = _mapper.Map<RolIzinleri>(dto);
            _db.RolIzinleri.Add(ent);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = ent.Id }, _mapper.Map<RolIzinleriDetailDto>(ent));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, RolIzinleriUpdateDto dto)
        {
            var ent = await _db.RolIzinleri.FirstOrDefaultAsync(x => x.Id == id);
            if (ent == null) return NotFound();

            _mapper.Map(dto, ent);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ent = await _db.RolIzinleri.FindAsync(id);
            if (ent == null) return NotFound();

            _db.RolIzinleri.Remove(ent);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
