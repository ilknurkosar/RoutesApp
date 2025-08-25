using System; // Convert için
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoutesService.API.Data;
using RoutesService.API.DTOs;
using RoutesService.Domain.Entities;

[Route("api/[controller]")]
[ApiController]
public class RotaResimTanimController : ControllerBase
{
    [Route("api/[controller]")]
    [ApiController]

    public class RotaResimTanimController : ControllerBase
    {
        private readonly RoutesDbContext _context;
        private readonly IMapper _mapper;

        public RotaResimTanimController(RoutesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // LISTE
        [HttpGet]
        public async Task<IEnumerable<RotaResimTanimListDto>> GetRotaResimler()
            => await _context.RotaResimler
                .AsNoTracking()
                .ProjectTo<RotaResimTanimListDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

        // DETAY
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RotaResimTanimDetailDto>> GetRotaResimTanim(int id)
        {
            var ent = await _context.RotaResimler.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (ent is null) return NotFound();
            return _mapper.Map<RotaResimTanimDetailDto>(ent);
        }

        // CREATE
        [HttpPost]
        public async Task<ActionResult<RotaResimTanimDetailDto>> PostRotaResimTanim(RotaResimTanimCreateDto dto)
        {
            var ent = _mapper.Map<RotaResimTanim>(dto);

            if (!string.IsNullOrWhiteSpace(dto.ResimBase64))
            {
                try
                {
                    ent.Resim = Convert.FromBase64String(dto.ResimBase64);
                }
                catch (FormatException)
                {
                    return BadRequest("Geçersiz ResimBase64.");
                }
            }

            _context.RotaResimler.Add(ent);
            await _context.SaveChangesAsync();

            var detail = _mapper.Map<RotaResimTanimDetailDto>(ent);
            return CreatedAtAction(nameof(GetRotaResimTanim), new { id = ent.Id }, detail);
        }

        // UPDATE
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutRotaResimTanim(int id, RotaResimTanimUpdateDto dto)
        {
            var ent = await _context.RotaResimler.FirstOrDefaultAsync(x => x.Id == id);
            if (ent is null) return NotFound();

            _mapper.Map(dto, ent);

            if (dto.ResimBase64 != null)
            {
                if (string.IsNullOrWhiteSpace(dto.ResimBase64))
                {
                    ent.Resim = null;
                }
                else
                {
                    try
                    {
                        ent.Resim = Convert.FromBase64String(dto.ResimBase64);
                    }
                    catch (FormatException)
                    {
                        return BadRequest("Geçersiz ResimBase64.");
                    }
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRotaResimTanim(int id)
        {
            var ent = await _context.RotaResimler.FindAsync(id);
            if (ent is null) return NotFound();

            _context.RotaResimler.Remove(ent);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
