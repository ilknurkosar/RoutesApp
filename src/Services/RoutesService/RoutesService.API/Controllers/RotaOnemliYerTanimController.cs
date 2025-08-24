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
    [Authorize] // JWT zorunlu
    public class RotaOnemliYerTanimController : ControllerBase
    {
        private readonly RoutesDbContext _db;
        private readonly IMapper _mapper;

        public RotaOnemliYerTanimController(RoutesDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<RotaOnemliYerTanimListDto>> Get()
        {
            return await _db.RotaOnemliYerler
                            .AsNoTracking()
                            .ProjectTo<RotaOnemliYerTanimListDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RotaOnemliYerTanimDetailDto>> Get(int id)
        {
            var ent = await _db.RotaOnemliYerler
                               .AsNoTracking()
                               .FirstOrDefaultAsync(x => x.Id == id);
            if (ent == null) return NotFound();
            return _mapper.Map<RotaOnemliYerTanimDetailDto>(ent);
        }

        [HttpPost]
        public async Task<ActionResult<RotaOnemliYerTanimDetailDto>> Create(RotaOnemliYerTanimCreateDto dto)
        {
            var ent = _mapper.Map<RotaOnemliYerTanim>(dto);
            _db.RotaOnemliYerler.Add(ent);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = ent.Id }, _mapper.Map<RotaOnemliYerTanimDetailDto>(ent));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, RotaOnemliYerTanimUpdateDto dto)
        {
            var ent = await _db.RotaOnemliYerler.FirstOrDefaultAsync(x => x.Id == id);
            if (ent == null) return NotFound();

            _mapper.Map(dto, ent);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ent = await _db.RotaOnemliYerler.FindAsync(id);
            if (ent == null) return NotFound();

            _db.RotaOnemliYerler.Remove(ent);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
