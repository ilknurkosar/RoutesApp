using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoutesService.API.Data;
using RoutesService.API.DTOs;
using RoutesService.Domain.Entities;

namespace RoutesService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotaKategoriAtamaController : ControllerBase
    {
        private readonly RoutesDbContext _db;
        private readonly IMapper _mapper;

        public RotaKategoriAtamaController(RoutesDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<RotaKategoriAtamaListDto>> Get()
        {
            return await _db.RotaKategoriAtama
                            .AsNoTracking()
                            .Include(x => x.Rota)
                            .Include(x => x.Kategori)
                            .ProjectTo<RotaKategoriAtamaListDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RotaKategoriAtamaDetailDto>> Get(int id)
        {
            var ent = await _db.RotaKategoriAtama
                               .AsNoTracking()
                               .Include(x => x.Rota)
                               .Include(x => x.Kategori)
                               .FirstOrDefaultAsync(x => x.Id == id);
            if (ent == null) return NotFound();
            return _mapper.Map<RotaKategoriAtamaDetailDto>(ent);
        }

        [HttpPost]
        public async Task<ActionResult<RotaKategoriAtamaDetailDto>> Create(RotaKategoriAtamaCreateDto dto)
        {
            var ent = _mapper.Map<RotaKategoriAtama>(dto);
            _db.RotaKategoriAtama.Add(ent);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = ent.Id }, _mapper.Map<RotaKategoriAtamaDetailDto>(ent));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, RotaKategoriAtamaUpdateDto dto)
        {
            var ent = await _db.RotaKategoriAtama.FirstOrDefaultAsync(x => x.Id == id);
            if (ent == null) return NotFound();

            _mapper.Map(dto, ent);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ent = await _db.RotaKategoriAtama.FindAsync(id);
            if (ent == null) return NotFound();

            _db.RotaKategoriAtama.Remove(ent);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
