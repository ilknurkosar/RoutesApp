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
    [Authorize]
    public class RotaYorumTanimController : ControllerBase
    {
        private readonly RoutesDbContext _context;
        private readonly IMapper _mapper;

        public RotaYorumTanimController(RoutesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<RotaYorumTanimListDto>> GetRotaYorumlar()
            => await _context.RotaYorumlar
                .AsNoTracking()
                .ProjectTo<RotaYorumTanimListDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

        // GET: api/RotaYorumTanim/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RotaYorumTanimDetailDto>> GetRotaYorumTanim(int id)
        {
            var dto = await _context.RotaYorumlar
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ProjectTo<RotaYorumTanimDetailDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (dto is null) return NotFound();
            return dto;
        }

        // POST: api/RotaYorumTanim
        [HttpPost]
        public async Task<ActionResult<RotaYorumTanimDetailDto>> PostRotaYorumTanim(RotaYorumTanimCreateDto dto)
        {
            // Basit sunucu doğrulamaları (opsiyonel ama faydalı)
            if (dto.Puan is < 1 or > 5)
                return BadRequest("Puan 1 ile 5 arasında olmalıdır.");
            if (dto.Yorum is { Length: > 500 })
                return BadRequest("Yorum en fazla 500 karakter olabilir.");

            var ent = _mapper.Map<RotaYorumTanim>(dto);
            _context.RotaYorumlar.Add(ent);
            await _context.SaveChangesAsync();

            var detail = _mapper.Map<RotaYorumTanimDetailDto>(ent);
            return CreatedAtAction(nameof(GetRotaYorumTanim), new { id = ent.Id }, detail);
        }

        // PUT: api/RotaYorumTanim/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutRotaYorumTanim(int id, RotaYorumTanimUpdateDto dto)
        {
            // Opsiyonel doğrulama
            if (dto.Puan is < 1 or > 5)
                return BadRequest("Puan 1 ile 5 arasında olmalıdır.");
            if (dto.Yorum is { Length: > 500 })
                return BadRequest("Yorum en fazla 500 karakter olabilir.");

            var ent = await _context.RotaYorumlar.FirstOrDefaultAsync(x => x.Id == id);
            if (ent is null) return NotFound();

            _mapper.Map(dto, ent);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/RotaYorumTanim/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRotaYorumTanim(int id)
        {
            var ent = await _context.RotaYorumlar.FindAsync(id);
            if (ent is null) return NotFound();

            _context.RotaYorumlar.Remove(ent);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
