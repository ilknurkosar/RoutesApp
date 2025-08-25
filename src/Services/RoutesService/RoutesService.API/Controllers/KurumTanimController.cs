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
    public class KurumTanimController : ControllerBase
    {
        private readonly RoutesDbContext _db;
        private readonly IMapper _mapper;

        public KurumTanimController(RoutesDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<KurumTanimListDto>> Get()
        {
            return await _db.Kurumlar
                            .AsNoTracking()
                            .ProjectTo<KurumTanimListDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<KurumTanimDetailDto>> Get(int id)
        {
            var ent = await _db.Kurumlar
                               .AsNoTracking()
                               .FirstOrDefaultAsync(x => x.Id == id);
            if (ent == null) return NotFound();
            return _mapper.Map<KurumTanimDetailDto>(ent);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<KurumTanimDetailDto>> Create(KurumTanimCreateDto dto)
        {
            var ent = _mapper.Map<KurumTanim>(dto);
            _db.Kurumlar.Add(ent);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = ent.Id }, _mapper.Map<KurumTanimDetailDto>(ent));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, KurumTanimUpdateDto dto)
        {
            var ent = await _db.Kurumlar.FirstOrDefaultAsync(x => x.Id == id);
            if (ent == null) return NotFound();

            _mapper.Map(dto, ent);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var ent = await _db.Kurumlar.FindAsync(id);
            if (ent == null) return NotFound();

            _db.Kurumlar.Remove(ent);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
