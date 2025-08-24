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
    public class RotaKategoriTanimController : ControllerBase
    {
        private readonly RoutesDbContext _db;
        private readonly IMapper _mapper;

        public RotaKategoriTanimController(RoutesDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<RotaKategoriTanimListDto>> Get()
        {
            return await _db.RotaKategoriler
                            .AsNoTracking()
                            .ProjectTo<RotaKategoriTanimListDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RotaKategoriTanimDetailDto>> Get(int id)
        {
            var ent = await _db.RotaKategoriler
                               .AsNoTracking()
                               .FirstOrDefaultAsync(x => x.Id == id);
            if (ent == null) return NotFound();
            return _mapper.Map<RotaKategoriTanimDetailDto>(ent);
        }

        [HttpPost]
        public async Task<ActionResult<RotaKategoriTanimDetailDto>> Create(RotaKategoriTanimCreateDto dto)
        {
            var ent = _mapper.Map<RotaKategoriTanim>(dto);
            _db.RotaKategoriler.Add(ent);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = ent.Id }, _mapper.Map<RotaKategoriTanimDetailDto>(ent));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, RotaKategoriTanimUpdateDto dto)
        {
            var ent = await _db.RotaKategoriler.FirstOrDefaultAsync(x => x.Id == id);
            if (ent == null) return NotFound();

            _mapper.Map(dto, ent);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ent = await _db.RotaKategoriler.FindAsync(id);
            if (ent == null) return NotFound();

            _db.RotaKategoriler.Remove(ent);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
